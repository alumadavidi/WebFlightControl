using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class ExternalFlight : IExternalFlight
    {
        private IDataManager db;
      //  private SqliteDB db = SqliteDB.Instance;
        private static Dictionary<string, string> flightplanServer;
        //private string url;
        //private List<Flight> flight;



        public ExternalFlight(IDataManager db)
        {
            this.db = db;  
           
        }

        public async Task<FlightPlan> GetExternalFlightPlanAsync(string id)
        {
            FlightPlan flightPlan = null;
            try {
                //get the server that has the flightPlan
                string serverId = flightplanServer[id];
                ServerFlight s =  db.GetServerById(serverId);
                if(serverId == null)
                {
                    return null;
                }
                string url = "api/FlightPlan/" + id;
                HttpClient httpClient = BuildHttpClient(s);
                try
                {
                    string resp = await ResponceAsync(httpClient, url);
                    flightPlan = JsonConvert.DeserializeObject<FlightPlan>(resp);
                }
                catch (Exception)
                {
                    //Console.WriteLine("failed in external filghtPlan by id get response");
                }
            }
            catch //no server has the flight plan
            {
                flightPlan = null;
            }
            return flightPlan;
        }

        public async Task<List<Flight>> GetExternalFlightAsync(string time)
        {
            flightplanServer = new Dictionary<string, string>();
            List<Flight> flight = new List<Flight>();
            List<ServerFlight> servers = db.GetServers();
            string url = "api/Flights?relative_to=" + time;
            foreach (ServerFlight s in servers)
            {
                HttpClient httpClient = BuildHttpClient(s);
                try
                {
                   
                    string resp = await ResponceAsync(httpClient, url);
                    List<Flight> newFlight = JsonConvert.DeserializeObject<List<Flight>>(resp);
                    addToDic(newFlight, s.ServerId);
                    flight.AddRange(newFlight);
                }
                catch (Exception)
                {
                  Console.WriteLine("failed in external filght get response");
                }
            }
            ChangeStatusFlight(flight);
            return flight;
        }

        private void addToDic(List<Flight> listFlight, string serverId)
        {
            foreach(Flight f in listFlight)
            {
                flightplanServer.Add(f.Flight_Id, serverId);
            }
        }

        private async Task<string> ResponceAsync(HttpClient httpClient, string url)
        {
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string resp = await response.Content.ReadAsStringAsync();
            return resp;
        }
        private void ChangeStatusFlight(List<Flight> flight)
        {
           foreach(Flight f in flight){
                f.IsExternal = true;
           }
        }
        //build new Http client for each request from extenal server
        private HttpClient BuildHttpClient(ServerFlight s)
        {
            HttpClient httpClient = new HttpClient();
            
            httpClient.BaseAddress = new Uri(s.ServerUrl);
            //add header fields for jyson
            httpClient.DefaultRequestHeaders.Add("User-Agent", "C# console program");
            httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }
       
    }
}
