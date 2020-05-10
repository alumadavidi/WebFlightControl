﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        //private SqliteDB db = SqliteDB.Instance;
        private FlightPlanManager flightPlanManager = new FlightPlanManager();
        private FlightManager flightManager = new FlightManager();
        private SqliteDB db = SqliteDB.Instance;
        Flight f = new Flight("1234567", 33.240, 31.12, 216, "SwissAir1", "2020-12-26T23:56:21Z1"
            , false);
        Flight f1 = new Flight("1234568", 33.241, 31.12, 2165, "SwissAir2", "2020-12-26T23:56:21Z2"
            , false);
        Flight f2 = new Flight("1234569", 33.242, 31.12, 216, "SwissAir3", "2020-12-26T23:56:21Z3"
            , false);
        Flight f3 = new Flight("1234560", 33.243, 31.12, 216, "SwissAir4", "2020-12-26T23:56:21Z1"
            , false);




        //[HttpGet]
        //public void Get()
        //{
        //    flightPlanManager.Test();
        //}

        //GET /api/Flights? relative_to =< DATE_TIME >

        [HttpGet(Name = "GetAllFlight")]
        [Consumes("application/json")]

        public List<Flight> GetAllFlight([FromQuery(Name = "relative_to")] string relative_to)
        {
            List<Flight> flights = new List<Flight>();
            string query = Request.QueryString.Value;
            if (query.Contains("sync_all"))
            {
                flights = flightManager.GetAllFlights(relative_to);
            }
            else
            {
                flights = flightManager.GetFlightsFromServer(relative_to);
            }

            return flights;
        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            flightManager.DeleteFlight(id);
        }
    }
}