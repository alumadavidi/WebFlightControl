using System.Collections.Generic;
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
        public ActionResult<List<ServerFlight>> GetAllServer()
        {
            //get all the servers from DB
            List<ServerFlight> server = serverManager.GetServerFlights();
            if (server.Count != 0)
            {
                return Ok(server);
            }
            else
            {
                
                return NotFound();
            }
        }



        // POST: api/Servers
        [HttpPost]
        [Consumes("application/json")]
        public ActionResult Post([FromBody] ServerFlight s)
        {
            try 
            {
                //add new server to DB
                serverManager.AddServer(s);
                //created
                return Created("create new serverFlight", s);
            }
            catch
            {
                //InternalServerErrorResult
                return StatusCode(500);
            }
        }



        // DELETE: api/Servers/5
        
        [HttpDelete("{id}", Name = "DeleteServer")]
        [Consumes("application/json")]
        public ActionResult DeleteServer(string id)
        {
            try
            {
                //delete server from DB
                serverManager.deleteServer(id);
                return Ok();
            } catch
            {
                return NotFound(id);
            }
        }
    }
}
