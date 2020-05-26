using System.Collections.Generic;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightPlanController : ControllerBase
    {
        private IFlightPlanManager flightPlanManager;
        public FlightPlanController(IFlightPlanManager flight)
        {
            this.flightPlanManager = flight;
        }
      
        // GET: api/FlightPlan/5
        [HttpGet("{id}", Name = "GetFlightPlan")]
        [Consumes("application/json")]
        public async Task<ActionResult<FlightPlan>> GetFlightPlan(string id)
        {
            FlightPlan f = await flightPlanManager.GetFlightPlanById(id);
            if (f != null)
            {
                return Ok(f);
            } else
            {
                return NotFound(id);
            }
        }

        // POST: api/FlightPlan
        [HttpPost]
        [Consumes("application/json")]
        public ActionResult AddFlightPlan(FlightPlan f)
        {
            try
            {
                flightPlanManager.AddNewFlightPlan(f);
                return Created("create new FlightPlan", f);
            } 
            catch
            {
                //InternalServerErrorResult
                return StatusCode(500);
            }
        }
    }
}
