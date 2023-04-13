using Tasks;
using System.Collections.Generic;
using System.Linq;
using Tasks.Interface;
using System.Text.Json;

namespace Tasks.Services;

public class UserService:IuserInterfac {
    private List<User> users;
    private IWebHostEnvironment webHost;
    private string filePath;

    public UserService(IWebHostEnvironment webHost)
    {
        this.webHost = webHost;
        this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "User.json");
        using (var jsonFile = File.OpenText(filePath))
        {
            users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
    public User Login(User user) {
        User? userExist =null;
        foreach (User item in users)
        {  
            if(item.Equals(user)){
                userExist=item;
            }  
        }
        return userExist;
    }
    public List<User> GetAll() => users;
    public void Add(User user)
    {
        user.Id = users.Max(t => t.Id) + 1;
        users.Add(user);
        saveToFile();
    }
    public bool Delete(int id)
    {
        var user = users.FirstOrDefault(t => t.Id == id);
        if (user == null)
            return false;
        users.Remove(user);
        saveToFile();
        return true;
    }
    private void saveToFile()
    {
        File.WriteAllText(filePath, JsonSerializer.Serialize(users));
    }
}