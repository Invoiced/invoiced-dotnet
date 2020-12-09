using System.Net.Http;

namespace Invoiced
{
    public static class Client
    {
        static Client()
        {
            httpClient = new HttpClient();
        }

        internal static HttpClient httpClient { get; set; }
    }
}