using Tasks.Interface;

namespace Tasks.Services;

public class LogService : ILogService
{
    private readonly string filePath;
    StreamWriter sr;
    public LogService(IWebHostEnvironment web)
    {
        filePath = Path.Combine(web.ContentRootPath, "Logs", "tasts.log");
        sr = new StreamWriter(filePath, true);
    }
    public void Log(LogLevel level, string message)
    {
        lock(sr) {
            sr.WriteLine(
                $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} [{level}] {message}"); 
        }
    }

}

