using FlightControlWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class FlightPlanManager : IFlightPlanManager
    {
        //private SqliteDB db = SqliteDB.Instance;
        private readonly IDataManager db;
        private readonly IExternalFlight externalFlight;
        public FlightPlanManager(IDataManager db, IExternalFlight externalFlight)
        {
            this.db = db;
            this.externalFlight = externalFlight;
        }

        public void AddNewFlightPlan(FlightPlan flightPlan)
        {
            if (flightPlan == null || flightPlan.IsNull() ||
                    !TimeFunc.ValidStringDate(flightPlan.InitialLocation.DateTime))
            {
                throw new Exception();
            }
            db.AddFlightPlan(flightPlan, CreateId());
        }
        public async Task<FlightPlan> GetFlightPlanById(string id)
        {
            FlightPlan f =  db.GetFlightPlanById(id);
            if (f == null)
            {
                //try to get from extenal server
                try
                {
                    //ExternalFlight ex = new ExternalFlight();
                    f = await externalFlight.GetExternalFlightPlanAsync(id);
                }
                catch
                {
                    f = null;
                }
            }
            return f;
        }
       
        private string CreateId()
        {
            string id = "";
            string randomChar;
            Random rnd = new Random();
            for (int i = 0; i < 8; i++)
            {
                if (i < 2)
                {
                    randomChar = ""+(char)rnd.Next('A', 'Z');
                } else
                {
                    randomChar = rnd.Next(0, 9).ToString();
                }
                id += randomChar;
            }
            return id;
        }
    }
}
