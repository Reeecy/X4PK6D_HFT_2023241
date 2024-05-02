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
    public class EntriesExitsController : ControllerBase
    {
        IEntriesExitsLogic logic;
        IHubContext<SignalRHub> hub;

        public EntriesExitsController(IEntriesExitsLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<EntriesExits> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public EntriesExits Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] EntriesExits value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("EntriesExitsCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] EntriesExits value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("EntriesExitsUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var entriesExitsToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("EntriesExitsDeleted", entriesExitsToDelete);
        }
    }
}
