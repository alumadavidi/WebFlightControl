using FlightControlWeb.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestFlightWeb.classTest
{
    class ExternalTest : IExternalFlight
    {
        public Task<List<Flight>> GetExternalFlightAsync(string time)
        {
            throw new NotImplementedException();
        }

        public Task<FlightPlan> GetExternalFlightPlanAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
