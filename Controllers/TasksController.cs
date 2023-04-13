using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks.Interface;

namespace Tasks.Controllers;
[ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private ItaskInterfac TaskService;
        public  TaskController(ItaskInterfac TaskService) {
            this.TaskService=TaskService;
        }
        [HttpGet]
        [Authorize(Policy = "User")]
        public IEnumerable<Task> Get()
        {
            string? token=Request.Headers.Authorization;
            return TaskService.GetAll(token);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "User")]
        public ActionResult<Task> Get(int id)
        {
            var p = TaskService.Get(id);
            if (p == null)
                return NotFound();
             return p;
        }

        [HttpPost]
        [Authorize(Policy = "User")]
        public ActionResult Post(Task task)
        {
            string? tokenStr = Request.Headers.Authorization;
            string newToken = tokenStr.Split(' ')[1];
            var token = new JwtSecurityToken(jwtEncodedString: newToken);
            string id = token.Claims.First(c => c.Type == "id").Value;
            task.userId=int.Parse(id);
            TaskService.Add(task);
            return CreatedAtAction(nameof(Post), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "User")]
        public ActionResult Put(int id, Task task)
        {
            if (! TaskService.Update(id, task))
                return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "User")]
        public ActionResult Delete (int id)
        {
            if (! TaskService.Delete(id))
                return NotFound();
            return NoContent();            
        }

    }
