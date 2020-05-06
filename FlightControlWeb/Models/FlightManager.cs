using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class FlightManager
    {
        private SqliteDB db = SqliteDB.Instance;

        public List<Flight> GetFlightsFromServer()
        {
            DateTime serverTime = DateTime.UtcNow;
            List<Flight> currentFlight = new List<Flight>();
            List<FlightPlanId> flightPlan = db.GetFlightPlans();

            foreach(FlightPlanId fid in flightPlan){
                FlightPlan f = fid.FlightP;
                DateTime clientTime = 
                    createDateTimeToClient(f.InitialLocationFlight.DataTime);
                List<Segment> segments = f.Segments;
                List<DateTime> dateTimes = createAllDataTimeClient(segments, clientTime);
                if (flightIsNow(clientTime, serverTime, dateTimes[dateTimes.Count -1]))
                {
                    int numSegment = findSpecificSegment(dateTimes, clientTime, serverTime);
                    //num seconds passed from the beginning of the section 
                    TimeSpan timeSpan = dateTimes[dateTimes.Count - 1] - serverTime;
                    double seconds = timeSpan.TotalSeconds;
                    //relative time from the beginning of the section
                    seconds = seconds / segments[numSegment].TimespanSeconds;
                    double longStart, longEnd, latStart, latEnd;
                    if (numSegment == 0)
                    {
                        longStart = f.InitialLocationFlight.Longitude;
                        longEnd = segments[numSegment].Longitude;

                        latStart = f.InitialLocationFlight.Latitude;
                        latEnd = segments[numSegment].Latitude;

                        
                    }
                    else 
                    {
                        longStart = segments[numSegment-1].Longitude;
                        longEnd = segments[numSegment].Longitude;

                        latStart = segments[numSegment - 1].Latitude;
                        latEnd = segments[numSegment].Latitude;

                    }
                    //Distance Distances
                    longEnd -= longStart;
                    latEnd -= latStart;
                    //current place
                    longStart += (longEnd * seconds);
                    latStart += (latEnd * seconds);
                    //add the new Flight to list
                    currentFlight.Add(new Flight(fid.ID, longStart, latStart,
                        f.Passengers, f.CompanyName, convertString(serverTime), false));
                }
            }
           
            return currentFlight;
        }

        private string convertString(DateTime serverTime)
        {
            return "" + serverTime.Year + "-" + serverTime.Month + "-" + serverTime.Day
                + "T" + serverTime.Hour + ":" + serverTime.Minute + ":" + serverTime.Minute
                + ":" + serverTime.Second;
        }

        private List<DateTime> createAllDataTimeClient(List<Segment> segments, DateTime start)
        {
            List<DateTime> dateTime = new List<DateTime>();
            for (int i = 0; i < segments.Count; i++)
            {
                dateTime.Add(start);
                start.AddSeconds(segments[i].TimespanSeconds);
            }
            dateTime.Add(start);
            return dateTime;
        }
        private int findSpecificSegment(List<DateTime> dateTimes, DateTime client,
            DateTime server)
        {
            int i;
            for(i = 1; i < dateTimes.Count; i++)
            {
                if(dateTimes[i-1] < server && server < dateTimes[i])
                {
                    break;
                }
            }
            return i-1;
        }
        private bool flightIsNow(DateTime client, DateTime server, DateTime end)
        {
            bool now = true;
            //doenst start or finish
            if((client > server) || (end < server))
            {
                now = false;
            }
            return now;
        }
      
        private DateTime createDateTimeToClient(string t)
        {
            //split the time from client
            string[] dataTime = t.Split("T");
            string[] data = dataTime[0].Split("-");
            string[] time = dataTime[1].Split(":");
            DateTime clientTime = new DateTime(Convert.ToInt32(data[0]),
                Convert.ToInt32(data[1]), Convert.ToInt32(data[2]),
                Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2]));
            //convert to +2 time zone
            clientTime.AddHours(2);
            return clientTime;

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
    }
}
