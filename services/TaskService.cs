using Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Tasks.Controllers
{   
    public static class TaskService
    {    
        private static List<Task> tasks = new List<Task>
        {
            new Task { Name="hw",Id=1, Done = false},
            new Task { Name="clean",Id=2, Done = true},
            new Task { Name="learn",Id=3, Done = false},
        };

        public static List<Task> GetAll() => tasks;
        public static Task Get(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return null;
            return task;
        }

        public static void Add(Task task)
        {
            task.Id = tasks.Max(t => t.Id) + 1;
            tasks.Add(task);
        }

        public static bool Update(int id, Task newTask)
        {
            if (newTask.Id != id)
                return false;
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return false;
            task.Name = newTask.Name;
            task.Done = newTask.Done;
            return true;
        }

        public static bool Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return false;
            tasks.Remove(task);
            return true;
        }
    }
}