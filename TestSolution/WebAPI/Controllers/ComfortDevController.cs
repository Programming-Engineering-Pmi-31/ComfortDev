using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComfortDevController : ControllerBase
    {
        ComfortDevTestContext db = new ComfortDevTestContext();

        [HttpGet("Topics")]
        public IEnumerable<Topics> GetTopics()
        {
            return db.Topics;
        }

        [HttpGet("Topics/{id}")]
        public IEnumerable<Topics> GetTopic(int id)
        {
            return db.Topics.Where(elem => elem.Id == id);
        }
    }
}
