using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwitchNotifier
{
    public class ConfigHandler
    {
        [JsonInclude]
        public string? TelegramToken { get; set; }
        [JsonInclude]
        public string? UserId { get; set; }
        [JsonInclude]
        public string? AuthToken { get; set; }

        private static readonly string ConfigPath = $"{Environment.CurrentDirectory}\\config.json";

        public static ConfigHandler? Read()
        {
            if (!File.Exists(ConfigPath))
                return null;
            ConfigHandler? configFile;
            using (FileStream fileStream = new FileStream(ConfigPath, FileMode.Open))
            {
                try
                {
                    configFile = JsonSerializer.Deserialize<ConfigHandler>(fileStream);
                }
                catch
                {
                    return null;
                }
            }
            return configFile;
        }
    }
}
