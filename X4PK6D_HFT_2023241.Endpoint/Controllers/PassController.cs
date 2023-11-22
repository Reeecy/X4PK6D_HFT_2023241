using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using X4PK6D_HFT_2023241.Logic;
using X4PK6D_HFT_2023241.Models;

namespace X4PK6D_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassController : ControllerBase
    {
        IPassLogic logic;

        public PassController(IPassLogic logic)
        {
            this.logic = logic;
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
        }

        [HttpPut]
        public void Update([FromBody] Pass value)
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
