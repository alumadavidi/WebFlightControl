using System.Collections.Generic;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly IServerManager serverManager;
        public ServersController(IServerManager s)
        {
            this.serverManager = s;
        }
        // GET: api/Server
        [HttpGet(Name = "GetAllServer")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<ServerFlight>>> GetAllServer()
        {
            //get all the servers from DB
            List<ServerFlight> server = serverManager.GetServerFlights();
            if (server.Count != 0)
            {
                return await Task.FromResult(Ok(server));
            }
            else
            {

                return await Task.FromResult(new List<ServerFlight>());
            }
        }



        // POST: api/Servers
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult> Post([FromBody] ServerFlight s)
        {
            try 
            {
                //add new server to DB
                serverManager.AddServer(s);
                //created
                return await Task.FromResult(Created("create new serverFlight", s));
            }
            catch
            {
                //InternalServerErrorResult
                return await Task.FromResult(StatusCode(500));
            }
        }



        // DELETE: api/Servers/5
        
        [HttpDelete("{id}", Name = "DeleteServer")]
        [Consumes("application/json")]
        public async Task<ActionResult> DeleteServer(string id)
        {
            try
            {
                //delete server from DB
                serverManager.deleteServer(id);
                return await Task.FromResult(Ok());
            } catch
            {
                return await Task.FromResult(NotFound(id));
            }
        }
    }
}
