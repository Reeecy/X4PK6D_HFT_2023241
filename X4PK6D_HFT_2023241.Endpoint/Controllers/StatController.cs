using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using X4PK6D_HFT_2023241.Logic;

namespace X4PK6D_HFT_2023241.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IPersonLogic logic;

        public StatController(IPersonLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public int? PersonCount()
        {
            return logic.PersonCount();
        }
    }
}
