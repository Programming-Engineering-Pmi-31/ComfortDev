using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComfortDev.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComfortDev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly TopicsService topicsService;

        public TopicsController()
        {
            topicsService = new TopicsService();
        }

        [HttpGet]
        public IActionResult GetTopics()
        {
            return Ok(topicsService.GetTopics());
        }
    }
}