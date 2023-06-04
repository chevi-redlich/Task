using Tasks.Interface;

namespace Tasks.Services;

public class LogService : ILogService
{
    private readonly string filePath;

    public LogService(IWebHostEnvironment web)
    {
        filePath = Path.Combine(web.ContentRootPath, "Logs", "tasts.log");
    }
    public void Log(LogLevel level, string message)
    {
        using (var sr = new StreamWriter(filePath, true))
        {

            sr.WriteLine(
            $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} [{level}] {message}"); 
        }
    }

}

