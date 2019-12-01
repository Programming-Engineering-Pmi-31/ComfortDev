using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComfortDev.BLL.DTO;
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
        private readonly UserCoursesService userCoursesService;

        public UserCoursesController()
        {
            userCoursesService = new UserCoursesService();
        }

        [HttpPost("create")]
        public IActionResult CreateCourse([FromBody]int topicId)
        {
            try
            {
                int userId = int.Parse(User.Identity.Name);
                var createdCourse = userCoursesService.CreateCourse(userId, topicId, 3, 5);
                return Ok(createdCourse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("active")]
        public IActionResult GetActiveCourse()
        {
            try
            {
                int userId = int.Parse(User.Identity.Name);
                var activeCourse = userCoursesService.GetActiveCourse(userId);

                // revision required
                return Ok(new { courseTasks = activeCourse.CourseTasks, endDate = activeCourse.EndDate });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("setTaskCompPer")]
        public IActionResult SetTaskCompletionPercent(CourseTaskInfo courseTaskInfo)
        {
            try
            {
                userCoursesService.SetTaskCompletionPercent(courseTaskInfo.Id, courseTaskInfo.CompletionPercent);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}