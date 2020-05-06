using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class FlightManager
    {
        private SqliteDB db = SqliteDB.Instance;

        public List<Flight> GetFlightsFromServer()
        {
            //TODO: implement
            return null;
        }

        private List<Flight> GetExternalFlights()
        {
            //TODO: implement
            return null;
        }

        public List<Flight> GetAllFlights()
        {
            //TODO: implement
            return null;
        }

        public void DeleteFlight(string id)
        {
            db.RemoveFlightPlan(id);
        }

        private void GetTimeSpanInSegment()
        {
            //TODO
        }
        private void GetCurrentSegment()
        {
            //TODO
        }
        private void UpdateLongLate(Flight f)
        {
            //TODO
        }

    }
}
