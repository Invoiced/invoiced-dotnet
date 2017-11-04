using System;
using Xunit;
using Invoiced;
using System.Net.Http;
using System.Collections.Generic;


namespace InvoicedTest
{
    public class HttpUtilTest
    {
        [Fact]
        public void basicAuth()
        {
            var username = "";
            var password = "asdf2342342342";

            var auth = HttpUtil.basicAuth(username,password);
            var expectedAuth = "OmFzZGYyMzQyMzQyMzQy";
           
            Assert.True(auth == expectedAuth);
        
        }

        [Fact]
        public void getHeaders()
        {
            var resp = new HttpResponseMessage();
            var header1 = "TestValue1";
            var item1 = "Item1";
            var item2 = "Item2";
            IEnumerable<string> value1 =  new string[]{ item1,item2};

            resp.Headers.Add(header1,value1);

            var fetchedHeaderValues = HttpUtil.getHeaders(resp,header1);

            Assert.True( fetchedHeaderValues.Length == 2);

        }


        [Fact]
        public void getHeaderFirstValue() {

            var resp = new HttpResponseMessage();
            var header1 = "TestValue1";
            var item1 = "Item1";
            var item2 = "Item2";
            IEnumerable<string> value1 =  new string[]{ item1,item2};

            resp.Headers.Add(header1,value1);

            var fetchedHeaderValue = HttpUtil.getHeaderFirstValue(resp,header1);

            Assert.True(fetchedHeaderValue == item1);
        

        }



    }
}
