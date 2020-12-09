using System.Net.Http;
using RichardSzalay.MockHttp;

namespace InvoicedTest
{
    public class JsonMatcher : IMockedRequestMatcher
    {
        private readonly string content;


        public JsonMatcher(string content)
        {
            this.content = content;
        }

        public bool Matches(HttpRequestMessage message)
        {
            if (message.Content == null)
                return false;

            var actualContent = message.Content.ReadAsStringAsync().Result;

            return JsonUtil.JsonEqual(actualContent, content);
        }
    }
}