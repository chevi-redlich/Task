using Tasks;
using Tasks.Interface;
using Tasks.Controllers;
using Tasks.Services;

namespace Tasks.Utilities
{
    public static class helper {
        public static void AddTask(this IServiceCollection services) {
            services.AddSingleton<ItaskInterfac,TaskService>();
           
        }
        public static void AddUser(this IServiceCollection services)
        {
            services.AddSingleton<IuserInterfac,UserService>();
        }
        public static void login(this IServiceCollection services)
        {
            services.AddTransient<ILogService, LogService>();
        }

       
    }
}