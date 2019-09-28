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
        public IEnumerable<Topic> GetTopics()
        {
            return db.Topic;
        }

        [HttpGet("Topics/{id}")]
        public IEnumerable<Topic> GetTopic(int id)
        {
            return db.Topic.Where(elem => elem.Id == id);
        }
    }
}
