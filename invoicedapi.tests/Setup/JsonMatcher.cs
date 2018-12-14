using System.Net.Http;
using RichardSzalay.MockHttp;

namespace InvoicedTest
{
    public class JsonMatcher : IMockedRequestMatcher
    {
        private string content;

       
        public JsonMatcher(string content)
        {
            this.content = content;
        }

        public bool Matches(System.Net.Http.HttpRequestMessage message)
        {
            if (message.Content == null)
                return false;

            string actualContent = message.Content.ReadAsStringAsync().Result;

            return JsonUtil.JsonEqual(actualContent,content);
        }
    }
}