namespace TwitchNotifier.Request
{
    public class DropInfoRequest
    {
        public class PersistedQuery
        {
            public int version = 1;
            public string sha256Hash = "27f074f54ff74e0b05c8244ef2667180c2f911255e589ccd693a1a52ccca7367";
        }

        public class Extensions
        {
            public PersistedQuery persistedQuery = new();
        }

        public string operationName = "Inventory";
        public object? variables;
        public Extensions extensions = new();
    }

    public class AllCampaignsInfoRequest
    {
        public class PersistedQuery
        {
            public int version = 1;
            public string sha256Hash = "e8b98b52bbd7ccd37d0b671ad0d47be5238caa5bea637d2a65776175b4a23a64";
        }

        public class Extensions
        {
            public PersistedQuery persistedQuery = new();
        }

        public string operationName = "DropCampaignDetails";
        public object? variables;
        public Extensions extensions = new();

    }

    public class AllCampaignsRequest
    {
        public class Extensions
        {
            public PersistedQuery persistedQuery = new();
        }

        public class Input
        {
            public string sessionID { get; set; }
            public string availability { get; set; }
            public object activity { get; set; }
        }

        public class PersistedQuery
        {
            public int version = 1;
            public string sha256Hash = "e8b98b52bbd7ccd37d0b671ad0d47be5238caa5bea637d2a65776175b4a23a64";
        }

        public class Root
        {
            public string operationName  = "DropCampaignDetails";
            public Variables variables { get; set; }
            public Extensions extensions { get; set; }
        }

        public class Variables
        {
            public string dropID { get; set; }
            public string channelLogin { get; set; }
            public Input input { get; set; }
        }
    }
}
