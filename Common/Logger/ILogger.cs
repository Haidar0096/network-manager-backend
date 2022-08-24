namespace NetworkManagerApi.Common.Logger
{
    public interface ILogger
    {
        void Log(LogLevel level, string message);
    }

    public enum LogLevel
    {
        info,
        warning,
        error,
        nothing
    }
}
