using FlightControlWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightControlWeb.Controllers
{
    public interface IFlightManager
    {
        public List<Flight> GetFlightsFromServer(string relative_to);
        public Task<List<Flight>> GetAllFlights(string relative_to);
        public void DeleteFlight(string id);
    }
}