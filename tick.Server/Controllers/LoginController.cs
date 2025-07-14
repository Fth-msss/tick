using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using tick.Server.Models;
using tick.Server.Models.views;


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
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.UserName) }),
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
                return BadRequest("Email already in use.");
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

        [HttpPost("createTicket")]
        public IActionResult CreateTicket([FromBody] CreateTicketView dto)
        {
            //only call this after locking the ticket

            //create the ticket
            var ticket = new Ticket
            {
            Owner = dto.OwnerName,
       
            CreatedAt = DateTime.UtcNow,
       
            State = "Valid",
      
            EventId = dto.IdEvent,
       
            SeatId = dto.IdSeat
            };

           
            _context.Ticket.Add(ticket);
            _context.SaveChanges();

            return Ok("Ticket created.");
        }
    }



}
