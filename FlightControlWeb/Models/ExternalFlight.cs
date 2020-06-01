using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class ExternalFlight : IExternalFlight
    {
        private readonly IDataManager db;
        private static Dictionary<string, string> flightplanServer;

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
                //build new http client to new request
                HttpClient httpClient = BuildHttpClient(s);
                try
                {
                    //get string response and convert to object flight plan
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
                //build new http client to new request
                HttpClient httpClient = BuildHttpClient(s);
                try
                {
                    //get string response and convert to object flight plan
                    string resp = await ResponceAsync(httpClient, url);
                    List<Flight> newFlight = JsonConvert.DeserializeObject<List<Flight>>(resp);
                    AddToDic(newFlight, s.ServerId);
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

        private void AddToDic(List<Flight> listFlight, string serverId)
        {
            try
            {
                //add new server and flight id to dic
                foreach (Flight f in listFlight)
                {
                    flightplanServer.Add(f.Flight_Id, serverId);
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        private async Task<string> ResponceAsync(HttpClient httpClient, string url)
        {
            //Send request
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            //get response
            string resp = await response.Content.ReadAsStringAsync();
            return resp;
        }
        private void ChangeStatusFlight(List<Flight> flight)
        {
            //change status flight of external to true
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

        public void RemoveServerFromDic(string id)
        {
            try
            {
                //remove all  server from dic if it reomve
                foreach (var item in
                flightplanServer.Where(kvp => kvp.Value == id).ToList())
                {
                    flightplanServer.Remove(item.Key);
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
