using System.Collections.Generic;
using System.Net.Http;
using Invoiced;
using Xunit;

namespace InvoicedTest
{
    public class HttpUtilTest
    {
        [Fact]
        public void BasicAuth()
        {
            var username = "";
            var password = "asdf2342342342";

            var auth = HttpUtil.BasicAuth(username, password);
            var expectedAuth = "OmFzZGYyMzQyMzQyMzQy";

            Assert.True(auth == expectedAuth);
        }

        [Fact]
        public void GetHeaders()
        {
            var resp = new HttpResponseMessage();
            var header1 = "TestValue1";
            var item1 = "Item1";
            var item2 = "Item2";
            IEnumerable<string> value1 = new[] {item1, item2};

            resp.Headers.Add(header1, value1);

            var fetchedHeaderValues = HttpUtil.GetHeaders(resp, header1);

            Assert.True(fetchedHeaderValues.Length == 2);
        }


        [Fact]
        public void GetHeaderFirstValue()
        {
            var resp = new HttpResponseMessage();
            var header1 = "TestValue1";
            var item1 = "Item1";
            var item2 = "Item2";
            IEnumerable<string> value1 = new[] {item1, item2};

            resp.Headers.Add(header1, value1);

            var fetchedHeaderValue = HttpUtil.GetHeaderFirstValue(resp, header1);

            Assert.True(fetchedHeaderValue == item1);
        }
    }
}