using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using X4PK6D_HFT_2023241.Endpoint.Services;
using X4PK6D_HFT_2023241.Logic;
using X4PK6D_HFT_2023241.Models;

namespace X4PK6D_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassController : ControllerBase
    {
        IPassLogic logic;
        IHubContext<SignalRHub> hub;

        public PassController(IPassLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Pass> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Pass Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Pass value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("PassCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Pass value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("PassUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var passToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("PassDeleted", passToDelete);
        }
    }
}
