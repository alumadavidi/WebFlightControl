using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public interface IExternalFlight
    {
        public Task<FlightPlan> GetExternalFlightPlanAsync(string id);
        public Task<List<Flight>> GetExternalFlightAsync(string time);
        public void RemoveServerFromDic(string id);
    }
}