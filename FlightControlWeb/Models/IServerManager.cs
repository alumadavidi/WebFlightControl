using System.Collections.Generic;

namespace FlightControlWeb.Models
{
    public interface IServerManager
    {
        public void AddServer(ServerFlight s);
        public void deleteServer(string id);
        public List<ServerFlight> GetServerFlights();
    }
}