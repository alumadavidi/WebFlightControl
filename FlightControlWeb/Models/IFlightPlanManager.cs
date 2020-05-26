using FlightControlWeb.Models;
using System.Threading.Tasks;

namespace FlightControlWeb.Controllers
{
    public interface IFlightPlanManager
    {
        public void AddNewFlightPlan(FlightPlan flightPlan);
        public Task<FlightPlan> GetFlightPlanById(string id);
    }
}