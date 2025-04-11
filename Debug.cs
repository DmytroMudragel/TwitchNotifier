
namespace TwitchNotifier
{
    public static class Debug
    {
        private readonly static string DebugPath = Environment.CurrentDirectory + "\\Debug.txt";

        public static void AddDebugRecord(string text)
        {
            if (Console.GetCursorPosition().Top == 1000)
            {
                Console.Clear();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{DateTime.Now} {text}");
            StreamWriter streamWriter = File.AppendText(DebugPath);
            streamWriter.WriteLine($"{DateTime.Now} {text}");
            streamWriter.Close();
        }

        public static void ClearLog()
        {
            if (File.Exists(DebugPath))
                File.Delete(DebugPath);
        }
    }
}
