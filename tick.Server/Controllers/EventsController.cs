using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tick.Server.Models;

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
        public async Task<ActionResult<IEnumerable<Seat>>> GetSeats() 
        {
            //1:how to do the query: get event from user, get seatlayout, get seats.
            //2:also get tickets, get the event, get the layout, get seats.
            //3: get seatlock, get event, get layout, get seats.
            //query where 1+2= full seats, 1+3= busy seats, and the rest is avaible tickets 

            var seats = await _context.Seat.ToListAsync();
            return Ok(seats);
        }
    }
}
