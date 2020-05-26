using System.Collections.Generic;

namespace FlightControlWeb.Models
{
    public interface IDataManager
    {
        public void AddServer(ServerFlight s);
        public void AddFlightPlan(FlightPlan f, string id);
        public List<ServerFlight> GetServers();
        public List<FlightPlanId> GetFlightPlans();
        public FlightPlan GetFlightPlanById(string id);
        public ServerFlight GetServerById(string id);
        public void RemoveFlightPlan(string id);
        public void RemoveServer(string id);
    }
}
