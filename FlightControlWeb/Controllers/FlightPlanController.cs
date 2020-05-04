using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightPlanController : ControllerBase
    {
        private FlightPlanManager flightPlanManager = new FlightPlanManager();
       
        // GET: api/FlightPlan/5
        [HttpGet("{id}", Name = "Get")]
        public FlightPlan GetFlightPlan(string id)
        {
            return flightPlanManager.GetFlightPlanById(id);
        }

        // POST: api/FlightPlan
        [HttpPost]
        public FlightPlan AddFlightPlan(FlightPlan f)
        {
            flightPlanManager.AddNewFlightPlan(f);
            return f;
        }
    }
}
