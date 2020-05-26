using FlightControlWeb.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestFlightWeb.classTest
{
    public class InnerDb : IDataManager
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
            return null;
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
