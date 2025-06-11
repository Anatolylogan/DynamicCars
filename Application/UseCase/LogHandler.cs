namespace Application.UseCase
{
    public delegate void LogHandler(string message);

    public static class Logger
    {
        public static void LogToConsole(string message)
        {
            Console.WriteLine($"[LOG - {DateTime.Now}]: {message}");
        }
        public static void LogToFile(string message)
        {
            string logFilePath = "log.txt";
            File.AppendAllText(logFilePath, $"[LOG - {DateTime.Now}]: {message}{Environment.NewLine}");
        }
    }

}
