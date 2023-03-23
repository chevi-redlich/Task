namespace Tasks.Interface;
public interface ILogService
{
    void Log(LogLevel level, string message);
}