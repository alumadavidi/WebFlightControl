using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightControlWeb.Controllers
{//
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
        private static FlightPlan f = new FlightPlan("123450", 216, "swir1",
            new InitialLocation(33.244, 31.12, "2020-12-26T23:56:21Z5"),
            s);
        private static FlightPlan f1 = new FlightPlan("123451", 216, "swir",
            new InitialLocation(33.244, 31.12, "2020-12-26T23:56:21Z6"),
            s0);
        private static FlightPlan f2 = new FlightPlan("123452", 216, "swir1",
            new InitialLocation(33.244, 31.12, "2020-12-26T23:56:21Z7"),
            s1);
        private static FlightPlan f3 = new FlightPlan("123453", 216, "swir",
            new InitialLocation(33.244, 31.12, "2020-12-26T23:56:21Z8"),
            s2);

        // GET: api/FlightPlan/5
        [HttpGet("{id}", Name = "GetFlightPlan")]
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
