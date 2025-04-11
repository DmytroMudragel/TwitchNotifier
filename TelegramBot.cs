using Telegram.Bot;

namespace TwitchNotifier
{
    public class TelegramBot
    {
        private static string TelegramToken = "";
        private static string UserId = "";

        public static ITelegramBotClient? botClient;

        [Obsolete]
        public static void Init(string token,string Userid)
        {
            TelegramToken = token;
            UserId = Userid;
            try
            {
                botClient = new TelegramBotClient(TelegramToken) { Timeout = TimeSpan.FromSeconds(10) };
                var Me = botClient.GetMeAsync().Result;
                if (Me != null && !string.IsNullOrEmpty(Me.FirstName))
                {
                    Debug.AddDebugRecord("Telegram bot was successfully loaded");
                    SendMessage("TwitchNotifier was successfully loaded");
                }
            }
            catch (Exception ex)
            {
                Debug.AddDebugRecord(ex.Message.ToString());
            }
        }

        public static void SendPhoto(string text, string url)
        {
            botClient.SendPhotoAsync(chatId: UserId, photo: url, caption: text);
        }

        public static void SendMessage(string text)
        {
            botClient.SendTextMessageAsync(chatId: UserId, text:text);
        }
    }
}


