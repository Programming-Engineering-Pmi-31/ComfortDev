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
    public class TestController : ControllerBase
    {
        private readonly TestService testService;

        public TestController()
        {
            testService = new TestService();
        }

        [HttpGet]
        public IActionResult GetTest()
        {
            return Ok(testService.GetTest());
        }
    }
}