
using Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Tasks.Interface
{   
    public interface ItaskInterfac
    {    
        
        public  List<Task> GetAll();

        public Task Get(int id);

        public void Add(Task task);

        public bool Update(int id, Task newTask);

        public bool Delete(int id);
    }
}