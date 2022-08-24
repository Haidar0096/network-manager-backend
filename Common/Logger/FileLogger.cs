using System;
using System.IO;
using System.Text;

namespace NetworkManagerApi.Common.Logger
{
    public class FileLogger : ILogger
    {
        public FileLogger() { }
        public async void Log(LogLevel level, string message)
        {
            string appBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            char sep = Path.DirectorySeparatorChar;

            StreamWriter file = new StreamWriter($"{appBaseDirectory}{sep}Logs{sep}logs.txt", append: true);
            using (file)
            {
                StringBuilder s = new StringBuilder();
                s
                    .Append("[")
                    .Append(DateTime.UtcNow.ToString())
                    .Append("]")
                    .Append("[")
                    .Append("Level: ")
                    .Append(level)
                    .Append(']')
                    .Append("[")
                    .Append("Message: ")
                    .Append(message)
                    .Append("]");
                await file.WriteLineAsync(s.ToString());
            }
        }
    }
}