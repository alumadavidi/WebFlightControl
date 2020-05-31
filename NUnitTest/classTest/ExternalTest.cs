using FlightControlWeb.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestFlightWeb.classTest
{
    public class ExternalTest : IExternalFlight
    {
        private readonly IDataManager data;
        public ExternalTest(IDataManager d)
        {
            data = d;
        }
        public Task<List<Flight>> GetExternalFlightAsync(string time)
        {
            throw new NotImplementedException();
        }

        public async Task<FlightPlan> GetExternalFlightPlanAsync(string id)
        {
            FlightPlan f = data.GetFlightPlanById(id);
            return f;
        }

        public void RemoveServerFromDic(string id)
        {
            throw new NotImplementedException();
        }
    }
}
