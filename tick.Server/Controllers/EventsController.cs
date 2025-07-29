using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tick.Server.Models;
using tick.Server.Models.views;

namespace tick.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {

        AppDbContext _context;
        public EventsController(AppDbContext context) 
        {
            _context = context;
        }

        [HttpGet("getEvents")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            var events = await _context.Event.ToListAsync();
            return Ok(events);
        }


        /// <summary>
        /// get physicalseatlayout from event table
        /// get seats from the physicalseatlayout table
        /// return seats
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("getEventSeats")]
        public async Task<ActionResult<IEnumerable<SeatStatusDto>>> GetSeats([FromQuery] int eventId)
        {
            // Step 1: get the layout ID for the given event
            var layoutId = await _context.Event
                .Where(e => e.Id == eventId)
                .Select(e => e.PhysicalLayoutId)
                .FirstOrDefaultAsync();

            if (layoutId == 0)
                return NotFound("Event not found or layout missing.");

            //get all seats in layout
            var seatStatuses = await (
                from seat in _context.Seat
                where seat.PhysicalLayoutId == layoutId

                // get tickets of event
                join ticket in _context.Ticket
                    .Where(t => t.EventId == eventId)
                    on seat.Id equals ticket.SeatId into ticketJoin
                from ticket in ticketJoin.DefaultIfEmpty()
                // Lock join: temporarily locked seats
                join lockItem in _context.Seatlock
                    .Where(sl => sl.EventId == eventId && sl.ValidUntil > DateTime.UtcNow)
                    on seat.Id equals lockItem.SeatId into lockJoin
                from lockItem in lockJoin.DefaultIfEmpty()

                select new SeatStatusDto
                {
                    SeatId = seat.Id,
                    SeatNumber = seat.Seatnumber,
                    Row = seat.Row,
                    SeatStatus = ticket != null ? "full"
                              : lockItem != null ? "busy"
                              : "available"
                }
            ).ToListAsync();

            return Ok(seatStatuses);
        }


    }
}
