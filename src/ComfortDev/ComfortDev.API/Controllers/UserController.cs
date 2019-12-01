using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ComfortDev.BLL.Services;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using ComfortDev.Common.Entities;
using ComfortDev.Common;
using ComfortDev.BLL.DTO;

namespace ComfortDev.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController()
        {
            userService = new UserService();
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(UserInfo userInfo)
        {
            try 
            {
                var userId = userService.Authenticate(userInfo.Username, userInfo.Password);

                var tokenHandler = new JwtSecurityTokenHandler();
                string key = Secrets.JwtKey;
                byte[] keyByte = HashConverter.PlainTextToBytes(key);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, userId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyByte), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { token = tokenString });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(UserInfo userInfo)
        {
            try
            {
                userService.Create(userInfo.Username, userInfo.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete()]
        public IActionResult Delete()
        {
            int id = int.Parse(User.Identity.Name);
            userService.Delete(id);
            return Ok();
        }
    }
}