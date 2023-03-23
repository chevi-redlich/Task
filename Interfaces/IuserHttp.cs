using Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Tasks.Interface
{
    public interface IuserInterfac
    {

        public User Login(User user);

        public List<User> GetAll();

        public void Add(User user);
        
        public bool Delete(int id);
    }
}