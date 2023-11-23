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

        [HttpGet]
        public IEnumerable<object> PersonsWithEntriesExits()
        {
            return logic.PersonsWithEntriesExits();
        }

        [HttpGet]
        public IEnumerable<object> PersonsWithExpiredPasses()
        {
            return logic.PersonsWithExpiredPasses();
        }

        [HttpGet]
        public IEnumerable<object> PersonsWithActivePasses()
        {
            return logic.PersonsWithActivePasses();
        }

        [HttpGet]
        public IEnumerable<object> PersonsWithMonthlyPassesAndTotalUsageDuration()
        {
            return logic.PersonsWithMonthlyPassesAndTotalUsageDuration();
        }

        [HttpGet]
        public IEnumerable<object> PersonsWithoutPasses()
        {
            return logic.PersonsWithoutPasses();
        }
    }
}
