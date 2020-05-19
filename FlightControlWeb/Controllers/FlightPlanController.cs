using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Web.Http.Results;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightPlanController : ControllerBase
    {
       // SqliteDB se = SqliteDB.Instance;
        private FlightManager flight = new FlightManager();
        private static List<Segment> s = new List<Segment>()
        {
            new Segment(33.234, 31.18,650)
        };
        private static List<Segment> s0 = new List<Segment>()
        {
            new Segment(33.234, 32,650),
             new Segment(33.23, 31.1,650),
             new Segment(254, 31.18,650)
        };
        private static List<Segment> s1 = new List<Segment>()
        {
            new Segment(88, 78,650)
        };
        private static List<Segment> s2 = new List<Segment>()
        {
            new Segment(5, 6,650),
             new Segment(8, 9,650)
        };
        private FlightPlanManager flightPlanManager = new FlightPlanManager();
        private static FlightPlan f = new FlightPlan(216, "swir1",
            new InitialLocation(33.244, 31.12, "2020-05-11T12:40:21Z"),
            s);
        private static FlightPlan f1 = new FlightPlan( 216, "swir",
            new InitialLocation(33.244, 31.12, "2020-05-11T12:40:21Z"),
            s0);
        private static FlightPlan f2 = new FlightPlan( 216, "swir1",
            new InitialLocation(33.244, 31.12, "2020-05-11T12:40:21Z"),
            s1);
        private static FlightPlan f3 = new FlightPlan( 216, "swir",
            new InitialLocation(33.244, 31.12, "2020-05-11T12:40:21Z"),
            s2);

        // GET: api/FlightPlan/5
        [HttpGet("{id}", Name = "GetFlightPlan")]
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

        public ActionResult AddFlightPlan(FlightPlan f)
        {
            try
            {
                flightPlanManager.AddNewFlightPlan(f);
                //flightPlanManager.AddNewFlightPlan(f1);
                //flightPlanManager.AddNewFlightPlan(f2);
                //flightPlanManager.AddNewFlightPlan(f3);
                //flight.GetFlightsFromServer("2020-05-11T12:45:21Z");

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
