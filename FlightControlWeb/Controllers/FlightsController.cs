using System.Collections.Generic;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightManager flightManager;
        public FlightsController(IFlightManager flight)
        {
            this.flightManager = flight;
        }

        //GET /api/Flights? relative_to =<DATE_TIME>

        [HttpGet(Name = "GetAllFlight")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<Flight>>> GetAllFlight(
            [FromQuery(Name = "relative_to")] string relative_to)
        {
            
            List<Flight> flights = new List<Flight>();
            string query = Request.QueryString.Value;
            if (query.Contains("sync_all"))
            {
                flights = await flightManager.GetAllFlights(relative_to);
            }
            else
            {
                flights = flightManager.GetFlightsFromServer(relative_to);
            }
            if (flights.Count != 0)
            {
                return Ok(flights);
            } else
            {
                return new List<Flight>();
            }

        }


        // DELETE: api/Flights/5
        [HttpDelete("{id}")]
        [Consumes("application/json")]
        public ActionResult Delete(string id)
        {
            try
            {
                flightManager.DeleteFlight(id);
                return Ok();
            } catch
            {
                return NotFound(id);
            }
        }
    }
}
