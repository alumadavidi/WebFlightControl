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
        private readonly IFlightPlanManager flightPlanManager;
        public FlightPlanController(IFlightPlanManager flight)
        {
            this.flightPlanManager = flight;
        }
      
        // GET: api/FlightPlan/5
        [HttpGet("{id}", Name = "GetFlightPlan")]
        [Consumes("application/json")]
        public async Task<ActionResult<FlightPlan>> GetFlightPlan(string id)
        {
            //get flight by id
            FlightPlan f = await flightPlanManager.GetFlightPlanById(id);
            if (f != null)
            {
                return await Task.FromResult(Ok(f));
            } else
            {
                //the id not found
                return await Task.FromResult(NotFound(id));
            }
        }

        // POST: api/FlightPlan
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult> AddFlightPlan(FlightPlan f)
        {
            try
            {
                //add new flight plan to DB
                flightPlanManager.AddNewFlightPlan(f);
                return await Task.FromResult(Created("create new FlightPlan", f));
            } 
            catch
            {
                //InternalServerErrorResult
                return await Task.FromResult(StatusCode(500));
            }
        }
    }
}
