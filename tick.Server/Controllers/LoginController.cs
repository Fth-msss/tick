using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using tick.Server.Models;
using tick.Server.Models.views;

///contoller that governs over logging in/registering. 
///also governs ticket creation and seat reservation
namespace tick.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class LoginController :ControllerBase
    {
        AppDbContext _context;
        private readonly IConfiguration _config;
        public LoginController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;

        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserView dto)
        {
            //password check
            var user = _context.User.SingleOrDefault(u => u.UserName == dto.UserName);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return Unauthorized("Invalid credentials");
            }

            // Create JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Secret"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                { 
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);
            return Ok(new { token = jwt });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserView dto)
        {
            if (_context.User.Any(u => u.UserName == dto.UserName))
            {
                return BadRequest("user with the same name already exists");
            }

            var user = new User
            {
                UserName = dto.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.User.Add(user);
            _context.SaveChanges();

            return Ok("User registered.");
        }

        [Authorize]
        [HttpPost("reserveSeat")]
        public async Task<IActionResult> ReserveSeat([FromBody] CreateTicketView dto)
        {
            //check if token data matches user
            var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userClaim == null || !int.TryParse(userClaim.Value, out int userId))
                return Unauthorized("Invalid user ID, user:" + userClaim);

            //check if there is already a ticket for the seat
            var ticket = await _context.Ticket.FirstOrDefaultAsync(t => t.EventId == dto.IdEvent && t.SeatId == dto.IdSeat && t.State == "Valid");
            if (ticket != null) { return BadRequest("seat already has a valid ticket"); }

            //query for necessary data
            //gets the event, the seat, and if it exists, seat lock of seat
            var eventSeatData = (
                from ev in _context.Event
                where ev.Id == dto.IdEvent
                from st in _context.Seat
                where st.Id == dto.IdSeat
                join sl in _context.Seatlock 
                on new { EventId = ev.Id, SeatId = st.Id } equals new { sl.EventId, sl.SeatId } into seatlockJoin
                from slock in seatlockJoin.DefaultIfEmpty()
                select new
                {
                    Event = ev,
                    Seat = st,
                    SeatLock = slock
                }
            ).FirstOrDefault();
            if (eventSeatData == null)
                return BadRequest("Event or seat not found");

            // query user. 
            var user = _context.User.Find(userId);
            if (user == null)
                return Unauthorized("User not found");

            //check if seat is locked
            if(eventSeatData.SeatLock != null && eventSeatData.SeatLock.ValidUntil > DateTime.UtcNow) 
            {
                //if seat is locked by the same user: 
                if(eventSeatData.SeatLock.UserId == user.Id) 
                {
                    eventSeatData.SeatLock.ValidUntil = DateTime.UtcNow.AddMinutes(7);
                    await _context.SaveChangesAsync();
                    return Ok("user already locked this seat");
                    
                }
                else { return BadRequest("Seat locked by another user, how did you get here?"); }
            }
            else if (eventSeatData.SeatLock != null && eventSeatData.SeatLock.ValidUntil <= DateTime.UtcNow)
            {
                //remove any old invalid locks 
                _context.Seatlock.Remove(eventSeatData.SeatLock);
            }

            //reserve seat
            var seatlock = new Seatlock
            {
                EventId = dto.IdEvent,
                SeatId = dto.IdSeat,
                CreationTime = DateTime.UtcNow,
                ValidUntil = DateTime.UtcNow.AddMinutes(7),
                UserId = userId,
            };
            _context.Seatlock.Add(seatlock);
            await _context.SaveChangesAsync();
            return Ok("seat successfully locked");
        }

        [Authorize]
        [HttpPost("createTicket")]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketView dto)
        {
            // check if token has a valid user id
            var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userClaim == null || !int.TryParse(userClaim.Value, out int userId))
                return Unauthorized("Invalid user ID, user:" + userClaim);

            //check if user exists in database
            var user = await _context.User.FirstOrDefaultAsync(n => n.Id == userId);
            if(user == null) { return BadRequest("user not registered,somehow"); }

            //if ticket already exists
            var seatTaken = await _context.Ticket.AnyAsync(t => t.EventId == dto.IdEvent && t.SeatId == dto.IdSeat);
            if (seatTaken) { return BadRequest("seat is taken,somehow"); }

            //seat has to be locked by the same user
            var seatReady = await _context.Seatlock.AnyAsync(s => s.UserId == user.Id);
            if (!seatReady) { return BadRequest("seat is not locked by the same person"); }

            //create the ticket
            var ticket = new Ticket
            {
            Owner = user.UserName,
       
            CreatedAt = DateTime.UtcNow,
       
            State = "Valid",
      
            EventId = dto.IdEvent,
       
            SeatId = dto.IdSeat
            };
            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();
            return Ok(ticket);
        }
    }
}
