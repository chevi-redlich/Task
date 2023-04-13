using Tasks;
using System.Collections.Generic;
using System.Linq;
using Tasks.Interface;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Tasks.Services
{   
    public class TaskService: ItaskInterfac
    
    { 
        private IWebHostEnvironment  webHost;
        private string filePath;
        private List<Task> tasks;
        public TaskService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "tasks.json");
            using (var jsonFile = File.OpenText(filePath))
            {
                tasks = JsonSerializer.Deserialize<List<Task>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        public List<Task> GetAll(string tokenStr) {
            List<Task> tasksForUser = new List<Task>();
            string newToken = tokenStr.Split(' ')[1];
            var token = new JwtSecurityToken(jwtEncodedString: newToken);
            string id = token.Claims.First(c => c.Type == "id").Value;
            List<Task> tasks=new List<Task>();
            foreach (Task task in this.tasks)
            {
                if(task.userId.ToString()==id) {
                    tasksForUser.Add(task);
                }
            }
            return tasksForUser;
        } 
        public  Task? Get(int id )
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return null;
            return task;
        }

        public void Add(Task task)
        {
            task.Id = tasks.Max(t => t.Id) + 1;
            tasks.Add(task);
            saveToFile();
        }

        public bool Update(int id, Task newTask)
        {
            if (newTask.Id != id)
                return false;
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return false;
            task.Name = newTask.Name;
            task.Done = newTask.Done;
            saveToFile();
            return true;
        }

        public bool Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return false;
            tasks.Remove(task);
            saveToFile();
            return true;
        }
        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(tasks));
        }
    }
}