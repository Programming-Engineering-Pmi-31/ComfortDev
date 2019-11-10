using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComfortDev.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComfortDev.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserCoursesController : ControllerBase
    {
        private readonly UserCoursesService userService;

        public UserCoursesController()
        {
            userService = new UserCoursesService();
        }

        [HttpGet("create")]
        public IActionResult CreateCourse(int topicId)
        {
            try
            {
                int userId = int.Parse(User.Identity.Name);
                var createdCourse = userService.CreateCourse(userId, topicId, 3, 5);
                return Ok(createdCourse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}