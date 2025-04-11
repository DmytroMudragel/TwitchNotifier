namespace TwitchNotifier.Response
{
    public class DropInfoResponse
    {
        public class Self
        {
            public bool isAccountConnected { get; set; }
            public bool hasPreconditionsMet { get; set; }
            public int currentMinutesWatched { get; set; }
            public bool isClaimed { get; set; }
            public string? dropInstanceID { get; set; }
        }

        public class Game
        {
            public string? id { get; set; }
            public string? name { get; set; }
        }

        public class Channel
        {
            public string? id { get; set; }
            public string? name { get; set; }
            public string? url { get; set; }
        }

        public class Allow
        {
            public List<Channel>? channels { get; set; }
        }

        public class Campaign
        {
            public string? id { get; set; }
            public Self? self { get; set; }
        }

        public class TimeBasedDrop
        {
            public string? id { get; set; }
            public string? name { get; set; }
            public DateTime startAt { get; set; }
            public DateTime endAt { get; set; }
            public int requiredMinutesWatched { get; set; }
            public Self? self { get; set; }
            public Campaign? campaign { get; set; }
        }

        public class DropCampaignsInProgress
        {
            public string? id { get; set; }
            public DateTime startAt { get; set; }
            public DateTime endAt { get; set; }
            public string? name { get; set; }
            public string? status { get; set; }
            public Self? self { get; set; }
            public Game? game { get; set; }
            public Allow? allow { get; set; }
            public List<TimeBasedDrop>? timeBasedDrops { get; set; }
        }

        public class GameEventDrop
        {
            public Game? game { get; set; }
            public string? id { get; set; }
            public string? imageURL { get; set; }
            public bool isConnected { get; set; }
            public DateTime lastAwardedAt { get; set; }
            public string? name { get; set; }
            public string? requiredAccountLink { get; set; }
            public int totalCount { get; set; }
        }

        public class Inventory
        {
            public List<DropCampaignsInProgress>? dropCampaignsInProgress { get; set; }
            public List<GameEventDrop>? gameEventDrops { get; set; }
        }

        public class CurrentUser
        {
            public string? id { get; set; }
            public Inventory? inventory { get; set; }

        }

        public class Data
        {
            public CurrentUser? currentUser { get; set; }
        }


        public class Response
        {
            public Data? data { get; set; }
        }
    }

    public class AllDropInfoResponse
    {
        public class LocalizedToken
        {
            public string value { get; set; }
            public string __typename { get; set; }
        }

        public class Title
        {
            public string localizedFallback { get; set; }
            public List<LocalizedToken> localizedTokens { get; set; }
            public string __typename { get; set; }
        }

        public class BroadcastSettings
        {
            public string id { get; set; }
            public string title { get; set; }
            public string __typename { get; set; }
        }

        public class User
        {
            public string id { get; set; }
            public string login { get; set; }
            public string displayName { get; set; }
            public string profileImageURL { get; set; }
            public string primaryColorHex { get; set; }
            public BroadcastSettings broadcastSettings { get; set; }
            public string __typename { get; set; }
        }

        public class Broadcaster
        {
            public string id { get; set; }
            public BroadcastSettings broadcastSettings { get; set; }
            public string __typename { get; set; }
        }

        public class Self
        {
            public bool canWatch { get; set; }
            public bool isRestricted { get; set; }
            public object restrictionType { get; set; }
            public string __typename { get; set; }
            public bool isAccountConnected { get; set; }
        }

        public class Game
        {
            public string id { get; set; }
            public string displayName { get; set; }
            public string name { get; set; }
            public string __typename { get; set; }
            public string boxArtURL { get; set; }
        }

        public class Content
        {
            public string id { get; set; }
            public string previewImageURL { get; set; }
            public Broadcaster broadcaster { get; set; }
            public int viewersCount { get; set; }
            public Self self { get; set; }
            public Game game { get; set; }
            public string type { get; set; }
            public string __typename { get; set; }
        }

        public class Item
        {
            public string trackingID { get; set; }
            public string promotionsCampaignID { get; set; }
            public User user { get; set; }
            public string label { get; set; }
            public Content content { get; set; }
            public string __typename { get; set; }
        }

        public class PersonalSection
        {
            public string type { get; set; }
            public Title title { get; set; }
            public List<Item> items { get; set; }
            public string __typename { get; set; }
        }

        public class Owner
        {
            public string id { get; set; }
            public string name { get; set; }
            public string __typename { get; set; }
        }

        public class DropCampaign
        {
            public string id { get; set; }
            public string name { get; set; }
            public Owner owner { get; set; }
            public Game game { get; set; }
            public string status { get; set; }
            public DateTime startAt { get; set; }
            public DateTime endAt { get; set; }
            public string detailsURL { get; set; }
            public string accountLinkURL { get; set; }
            public Self self { get; set; }
            public string __typename { get; set; }
        }

        public class CurrentUser
        {
            public string id { get; set; }
            public string login { get; set; }
            public List<DropCampaign> dropCampaigns { get; set; }
            public string __typename { get; set; }
        }

        public class Data
        {
            public List<PersonalSection> personalSections { get; set; }
            public CurrentUser currentUser { get; set; }
        }

        public class Extensions
        {
            public int durationMilliseconds { get; set; }
            public string operationName { get; set; }
            public string requestID { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public Extensions extensions { get; set; }
        }




    }
}
