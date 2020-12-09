using RichardSzalay.MockHttp;

namespace InvoicedTest
{
    public static class JsonTypeExtensions
    {
        public static MockedRequest WithJson(
            this MockedRequest handler,
            string jsonObject)
        {
            handler.With(new JsonMatcher(jsonObject));

            return handler;
        }
    }
}