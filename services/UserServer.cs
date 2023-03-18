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
        //this.filePath = webHost.ContentRootPath+@"/Data/Pizza.json";
        using (var jsonFile = File.OpenText(filePath))
        {
            users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
    public bool Login(User user) {
        bool flag=false;
        
        foreach (User item in users)
        {
            if(item.Equals(user)){
                flag=true;
                System.Console.WriteLine("if in"); 
            }  
        }
        return flag;
        // User myUser=users.Find(t=>t.Id==user.Id);
        // if(user==null) {
        //     return false;
        // }
        // return true;
    }
}