using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private ServerManager serverManager = new ServerManager();
        // GET: api/Server
        [HttpGet(Name = "GetAllServer")]
        public List<ServerFlight> GetAllServer()
        {
            return serverManager.GetServerFlights();
        }



        // POST: api/Servers
        [HttpPost]
        public void Post([FromBody] ServerFlight s)
        {
            serverManager.AddServer(s);
        }



        // DELETE: api/Servers/5
        [HttpDelete("{id}", Name = "DeleteServer")]
        public void DeleteServer(string id)
        {
            serverManager.deleteServer(id);
        }
    }
}
