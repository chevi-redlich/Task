using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks.Controllers;
using Tasks.Services;
using Tasks;
using Tasks.Interface;

namespace Tasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        IuserInterfac userHttp;
        public AdminController(IuserInterfac IuserHttp) { 
            this.userHttp=IuserHttp;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<String> Login([FromBody] User User)
        {
            bool Exists=userHttp.Login(User);
            if(!Exists) {
                return Unauthorized();
            }
            var claims = new List<Claim>();
            if(User.isAdmin) {
                claims.Add(new Claim("type", "Admin"));
            }
            else {
                claims.Add(new Claim("type", "User"));
            }

            var token = UserTokenService.GetToken(claims);

            return new OkObjectResult(UserTokenService.WriteToken(token));
        }


        // [HttpPost]
        // [Route("[action]")]
        // [Authorize(Policy = "Admin")]
        // public IActionResult GenerateBadge([FromBody] Agent Agent)
        // {
        //     var claims = new List<Claim>
        //     {
        //         new Claim("type", "Agent"),
        //         new Claim("ClearanceLevel", Agent.ClearanceLevel.ToString()),
        //     };

        //     var token = FbiTokenService.GetToken(claims);

        //     return new OkObjectResult(FbiTokenService.WriteToken(token));
        // }
    }



}