using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
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
        public ActionResult<List<ServerFlight>> GetAllServer()
        {
            try
            {
                return Ok(serverManager.GetServerFlights());
            } catch
            {
                return NotFound();
            }
        }



        // POST: api/Servers
        [HttpPost]
        public ActionResult Post([FromBody] ServerFlight s)
        {
            try 
            {
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
        public ActionResult DeleteServer(string id)
        {
            try
            {
                serverManager.deleteServer(id);
                return Ok();
            } catch
            {
                return NotFound(id);
            }
        }
    }
}
