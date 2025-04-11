using System.Diagnostics;
using System.Timers;
using TwitchNotifier.Response;

namespace TwitchNotifier
{
    public static class Notifier
    {
        private static ConfigHandler? config;

        private static string? AuthToken;

        private static System.Timers.Timer? timer;

        private static List<AllDropInfoResponse.DropCampaign> dropsCampaignsPool = new List<AllDropInfoResponse.DropCampaign>();

        [Obsolete]
        public static bool Init()
        {
            Debug.ClearLog();
            Console.Title = "Twitch Notifier";
            if ((config = ConfigHandler.Read()) != null  
                && config.TelegramToken != null && config.UserId != null)
            {
                TelegramBot.Init(config.TelegramToken, config.UserId);
                AuthToken = config.AuthToken;
                timer = new System.Timers.Timer();
                timer.Elapsed += new ElapsedEventHandler(MainTask);
                timer.Interval = 60000;
                return true;
            }
            return false;
        }

        private static void MainTask(object source, ElapsedEventArgs e)
        {
            if (AuthToken != null)
            {
                TwitchAccount account = new TwitchAccount(AuthToken);
                AllDropInfoResponse.Root? AllDropsCampaignsInfo;
                if ((AllDropsCampaignsInfo = account.DropInfo()) != null && AllDropsCampaignsInfo.data != null
                    && AllDropsCampaignsInfo.data.currentUser != null && AllDropsCampaignsInfo.data.currentUser.dropCampaigns != null)
                {
                    int up = 0;
                    int ac = 0;
                    int ex = 0;
                    int tgSent = 0;
                    foreach (var item in AllDropsCampaignsInfo.data.currentUser.dropCampaigns)
                    {
                        Thread.Sleep(100);
                        var res = dropsCampaignsPool.FirstOrDefault(campaignItem => campaignItem.id == item.id);
                        switch (item.status)
                        {
                            case "UPCOMING":
                                up++;
                                if (res is null)
                                {
                                    DateTime startAt = item.startAt;
                                    if (startAt.Date == DateTime.Now.Date)
                                    {
                                        TelegramBot.SendPhoto($"{item.game.displayName} today at {startAt.ToLocalTime().ToString("HH:mm tt")}", item.game.boxArtURL);
                                        //TelegramBot.SendMessage($"{item.game.displayName} today at {startAt.ToLocalTime().ToString("HH:mm tt")}");
                                        Debug.AddDebugRecord($"{item.game.displayName} today at {startAt.ToLocalTime().ToString("HH:mm tt")}");
                                        tgSent++;
                                    }
                                    else if (startAt.Date.Day - DateTime.Now.Date.Day == 1)
                                    {
                                        //TelegramBot.SendMessage($"{item.game.displayName} tomorrow at {startAt.ToLocalTime().ToString("HH:mm tt")}");
                                        TelegramBot.SendPhoto($"{item.game.displayName} tomorrow at {startAt.ToLocalTime().ToString("HH:mm tt")}", item.game.boxArtURL);
                                        Debug.AddDebugRecord($"{item.game.displayName} tomorrow at {startAt.ToLocalTime().ToString("HH:mm tt")}");
                                        tgSent++;
                                    }
                                    else if (startAt.Date.Day - DateTime.Now.Date.Day == 2)
                                    {
                                        TelegramBot.SendPhoto($"{item.game.displayName} after tomorrow at {startAt.ToLocalTime().ToString("HH:mm tt")}", item.game.boxArtURL);
                                        //TelegramBot.SendMessage($"{item.game.displayName} after tomorrow at {startAt.ToLocalTime().ToString("HH:mm tt")}");
                                        Debug.AddDebugRecord($"{item.game.displayName} after tomorrow at {startAt.ToLocalTime().ToString("HH:mm tt")}");
                                        tgSent++;
                                    }
                                    dropsCampaignsPool.Add(item);
                                }
                                break;
                            case "ACTIVE":
                                ac++;
                                if (res is null)
                                {
                                    TelegramBot.SendPhoto($"{item.game.displayName} active\n{item.startAt.ToLocalTime().ToString("dd MMMM HH:mm tt")}\n{item.endAt.ToLocalTime().ToString("dd MMMM HH:mm tt")}", item.game.boxArtURL);
                                    Debug.AddDebugRecord($"{item.game.displayName} active\n{item.startAt.ToLocalTime().ToString("dd MMMM HH:mm tt")}\n{item.endAt.ToLocalTime().ToString("dd MMMM HH:mm tt")}");
                                    dropsCampaignsPool.Add(item);
                                    tgSent++;
                                }
                                else
                                {
                                    var temp = dropsCampaignsPool.FirstOrDefault(campaignItem => campaignItem.id == item.id, null);
                                    if (temp is not null && temp.status == "UPCOMING")
                                    {
                                        dropsCampaignsPool.Remove(temp);
                                        TelegramBot.SendPhoto($"{item.game.displayName} active\n{item.startAt.ToLocalTime().ToString("dd MMMM HH:mm tt")}\n{item.endAt.ToLocalTime().ToString("dd MMMM HH:mm tt")}", item.game.boxArtURL);
                                        Debug.AddDebugRecord($"{item.game.displayName} active\n{item.startAt.ToLocalTime().ToString("dd MMMM HH:mm tt")}\n{item.endAt.ToLocalTime().ToString("dd MMMM HH:mm tt")}");
                                        dropsCampaignsPool.Add(item);
                                        tgSent++;
                                    }
                                }
                                break;
                            case "EXPIRED":
                                ex++;
                                dropsCampaignsPool.Remove(item);
                                break;
                            default:
                                break;
                        }
                    }
                    if (AllDropsCampaignsInfo != null)
                    {
                        Debug.AddDebugRecord($"Up:{up} Ac:{ac} Ex:{ex} totalInSum:{up + ac + ex} totalInRequest:{AllDropsCampaignsInfo.data.currentUser.dropCampaigns.Count} Sent to tg:{tgSent}");
                    }
                }
            }
        }

        class Program
        {
            [Obsolete]
            static void Main(string[] args)
            {
                if (!Init())
                {
                    Debug.AddDebugRecord($"Error while reading config.");
                    return;
                }
                if (timer is not null)
                {
                    timer.Enabled = true;
                    //refreshStopwatch.Start();
                }
                Console.ReadLine();
                return;
            }
        }
    }
}

//    if (dropsCampaignsPool.FirstOrDefault(campaignItem => campaignItem.id == item.id) is not null)
//{
//    switch (item.status)
//    {
//        case "ACTIVE":
//            var res = dropsCampaignsPool.FirstOrDefault(campaignItem => campaignItem.id == item.id, null);
//            if (res is not null && res.status == "UPCOMING")
//            {

//                dropsCampaignsPool.Remove(res);
//                TelegramBot.SendMessage($"{item.game.displayName} active\n{item.startAt.ToLocalTime().ToString("dd MMMM HH:mm tt")}\n{item.endAt.ToLocalTime().ToString("dd MMMM HH:mm tt")}");
//                Debug.AddDebugRecord($"{item.game.displayName} active\n{item.startAt.ToLocalTime().ToString("dd MMMM HH:mm tt")}\n{item.endAt.ToLocalTime().ToString("dd MMMM HH:mm tt")}");
//                dropsCampaignsPool.Add(item);
//                up--;
//                ac++;
//            }
//            break;
//        case "EXPIREDE":
//            dropsCampaignsPool.Remove(item);
//            ac--;
//            ex++;
//            break;
//        default:
//            break;
//    }
//}
//else
//{
//    switch (item.status)
//    {
//        case "UPCOMING":
//            DateTime startAt = item.startAt;
//            if (startAt.Date == DateTime.Now.Date)
//            {
//                TelegramBot.SendMessage($"{item.game.displayName} today at {startAt.ToLocalTime().ToString("HH:mm tt")}");
//                Debug.AddDebugRecord($"{item.game.displayName} today at {startAt.ToLocalTime().ToString("HH:mm tt")}");
//            }
//            else if (startAt.Date.Day - DateTime.Now.Date.Day == 1)
//            {
//                TelegramBot.SendMessage($"{item.game.displayName} tomorrow at {startAt.ToLocalTime().ToString("HH:mm tt")}");
//                Debug.AddDebugRecord($"{item.game.displayName} tomorrow at {startAt.ToLocalTime().ToString("HH:mm tt")}");
//            }
//            else
//            {
//                TelegramBot.SendMessage($"{item.game.displayName} will be at {item.startAt.ToLocalTime().ToString("dd MMMM HH:mm tt")} - {item.endAt.ToLocalTime().ToString("dd MMMM HH:mm tt")}");
//                Debug.AddDebugRecord($"{item.game.displayName} will be at {item.startAt.ToLocalTime().ToString("dd MMMM HH:mm tt")} - {item.endAt.ToLocalTime().ToString("dd MMMM HH:mm tt")}");
//            }
//            dropsCampaignsPool.Add(item);
//            up++;
//            break;
//        case "ACTIVE":
//            TelegramBot.SendMessage($"{item.game.displayName} active\n{item.startAt.ToLocalTime().ToString("dd MMMM HH:mm tt")}\n{item.endAt.ToLocalTime().ToString("dd MMMM HH:mm tt")}");
//            Debug.AddDebugRecord($"{item.game.displayName} active\n{item.startAt.ToLocalTime().ToString("dd MMMM HH:mm tt")}\n{item.endAt.ToLocalTime().ToString("dd MMMM HH:mm tt")}");
//            dropsCampaignsPool.Add(item);
//            ac++;
//            break;
//        case "EXPIREDE":
//            dropsCampaignsPool.Remove(item);
//                ac--;
//                ex++;
//            break;
//        default:
//            break;
//    }
//}