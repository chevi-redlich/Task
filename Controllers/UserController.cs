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
    public class UserController : ControllerBase
    {
        IuserInterfac userHttp;
        public UserController(IuserInterfac IuserHttp) { 
            this.userHttp=IuserHttp;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<String> Login([FromBody] User user)
        {
            User userExist=userHttp.Login(user);
            if(userExist==null) {
                return Unauthorized();
            }
            var claims = new List<Claim>();
            if(user.isAdmin) {
                claims.Add(new Claim("type", "Admin"));
            }
            else {
                claims.Add(new Claim("type", "User"));
            }
            claims.Add(new Claim("id",user.Id.ToString()));
            claims.Add(new Claim("name", user.Name.ToString()));
            var token = UserTokenService.GetToken(claims);

            return new OkObjectResult(UserTokenService.WriteToken(token));
        }
        
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IEnumerable<User> Get()
        {
            return userHttp.GetAll();
        }
        
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public ActionResult Post(User user)
        {
            userHttp.Add(user);
            return CreatedAtAction(nameof(Post), new { id = user.Id }, user);
        }
        [HttpDelete("{id}")]
        [Authorize(Policy="Admin")]
        public ActionResult Delete(int id)
        {
            if (!userHttp.Delete(id))
                return NotFound();
            return NoContent();
        }
    }



}