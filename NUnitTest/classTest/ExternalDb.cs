using FlightControlWeb.Models;
using System;
using System.Collections.Generic;


namespace UnitTestFlightWeb.classTest
{
    public class ExternalDb : IDataManager
    {
        public void AddFlightPlan(FlightPlan f, string id)
        {
            throw new NotImplementedException();
        }

        public void AddServer(ServerFlight s)
        {
            throw new NotImplementedException();
        }

        public FlightPlan GetFlightPlanById(string id)
        {
            List<Segment> s = new List<Segment>()
            {
                new Segment(33.234, 31.18,650)
            };
            FlightPlan f = new FlightPlan(216, "swir1",
            new InitialLocation(33.244, 31.12, "2020-05-11T12:40:21Z"),
            s);

            return f;
        }

        public List<FlightPlanId> GetFlightPlans()
        {
            throw new NotImplementedException();
        }

        public ServerFlight GetServerById(string id)
        {
            throw new NotImplementedException();
        }

        public List<ServerFlight> GetServers()
        {
            throw new NotImplementedException();
        }

        public void RemoveFlightPlan(string id)
        {
            throw new NotImplementedException();
        }

        public void RemoveServer(string id)
        {
            throw new NotImplementedException();
        }
    }
}
