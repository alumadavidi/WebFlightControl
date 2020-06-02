using System;
using System.Collections.Generic;
using System.Threading;
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
                relative_to = relative_to.Split("&")[0];
                //get all filght - both form inner server and external server
                flights = await flightManager.GetAllFlights(relative_to);
            }
            else
            {
                //get flight just from inner server
                flights = flightManager.GetFlightsFromServer(relative_to);
            }
            if (flights.Count != 0)
            {
                return await Task.FromResult(Ok(flights));
            } else
            {
                return await Task.FromResult(new List<Flight>());
            }

        }


        // DELETE: api/Flights/5
        [HttpDelete("{id}")]
        [Consumes("application/json")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                //delete flight plan
                flightManager.DeleteFlight(id);
                return await Task.FromResult(Ok());
            } catch
            {
                return await Task.FromResult(NotFound(id));
            }
        }
    }
}
