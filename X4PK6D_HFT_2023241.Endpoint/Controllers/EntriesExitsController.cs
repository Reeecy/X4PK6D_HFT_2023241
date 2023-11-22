using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using X4PK6D_HFT_2023241.Logic;
using X4PK6D_HFT_2023241.Models;

namespace X4PK6D_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EntriesExitsController : ControllerBase
    {
        IEntriesExitsLogic logic;

        public EntriesExitsController(IEntriesExitsLogic logic)
        {
            this.logic = logic;
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
        }

        [HttpPut]
        public void Update([FromBody] EntriesExits value)
        {
            this.logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
