using RestSharp;
using System.Net;
using TwitchNotifier.Request;
using TwitchNotifier.Response;

namespace TwitchNotifier
{
    public class TwitchAccount : IDisposable
    {
        private const string CLIENTID = "kimne78kx3ncx6brgo4mv6wki5h1ko";
        private const string USERAGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4844.74 Safari/537.36 Edg/99.0.1150.55";
        public string AuthToken { get; private set; }
        public TwitchAccount(string authtoken)
        {
            AuthToken = authtoken;
        }
        public AllDropInfoResponse.Root? DropInfo()
        {
            var client = new RestClient("https://gql.twitch.tv/gql");
            client.UserAgent = USERAGENT;
            client.Timeout = 30_000;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "OAuth " + AuthToken);
            request.AddHeader("Client-Id", CLIENTID);
            request.AddJsonBody(new AllCampaignsInfoRequest());

            var resp = client.Post<AllDropInfoResponse.Root>(request);
            if (resp.StatusCode == HttpStatusCode.OK &&
                resp.Data != null)
                return resp.Data;

            return null;
        }
        public void Dispose() => GC.SuppressFinalize(this);
    }
}
