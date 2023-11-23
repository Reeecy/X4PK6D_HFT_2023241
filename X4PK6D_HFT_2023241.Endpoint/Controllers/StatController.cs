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

        [HttpGet("PersonCount")]
        public int? PersonCount()
        {
            return logic.PersonCount();
        }

        [HttpGet("PersonsWithEntriesAndExits")]
        public IEnumerable<object> PersonsWithEntriesExits()
        {
            return logic.PersonsWithEntriesExits();
        }

        [HttpGet("PersonsWithExpiredPasses")]
        public IEnumerable<object> PersonsWithExpiredPasses()
        {
            return logic.PersonsWithExpiredPasses();
        }

        [HttpGet("PersonsWithActivePasses")]
        public IEnumerable<object> StudentsWithActivePasses()
        {
            return logic.StudentsWithActivePasses();
        }

        [HttpGet("PersonsWithMonthlyPassesAndTotalUsage")]
        public IEnumerable<object> PersonsWithMonthlyPassesAndTotalUsageDuration()
        {
            return logic.PersonsWithMonthlyPassesAndTotalUsageDuration();
        }

        [HttpGet("PersonsWithoutPasses")]
        public IEnumerable<object> PersonsWithoutPasses()
        {
            return logic.PersonsWithoutPasses();
        }
    }
}
