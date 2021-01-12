using LoginAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private static User TheodorRisager => new User()
        {
            UserId = 1,
            EmailAddress = "theodor349@gmail.com",
            Password = "123",
        };

        [HttpPost("loginuser")]
        public async Task<ActionResult<User>> LoginUser(User user)
        {
            User loggedInUser = null;
            if(user.EmailAddress == "theodor349@gmail.com" && user.Password == "123")
            {
                loggedInUser = TheodorRisager;
            }

            if (loggedInUser != null)
            {
                //create a claim
                var claim = new Claim(ClaimTypes.Name, loggedInUser.EmailAddress);
                //create claimsIdentity
                var claimsIdentity = new ClaimsIdentity(new[] { claim }, "serverAuth");
                //create claimsPrincipal
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                //Sign In User
                await HttpContext.SignInAsync(claimsPrincipal);
            }
            return await Task.FromResult(loggedInUser);
        }

        [HttpGet("getcurrentuser")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            User currentUser = new User();

            if (User.Identity.IsAuthenticated)
            {
                currentUser = TheodorRisager;
            }

            return await Task.FromResult(currentUser);
        }

        [HttpGet("logoutuser")]
        public async Task<ActionResult<String>> LogOutUser()
        {
            await HttpContext.SignOutAsync();
            return "Success";
        }
    }
}
