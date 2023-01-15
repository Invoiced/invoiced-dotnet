using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Invoiced;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Xunit;

namespace InvoicedTest
{
    public class TaxRateTest
    {
        private static TaxRate CreateDefaultTaxRate(HttpClient client)
        {
            var json = @"{'id': 'alpha'}";

            var taxRate = JsonConvert.DeserializeObject<TaxRate>(json);

            var connection = new Connection("voodoo", Environment.test);

            connection.TestClient(client);

            taxRate.ChangeConnection(connection);

            return taxRate;
        }

        [Fact]
        public void TestDeserialize()
        {
            var json = @"{
				'created_at': 1574369950,
				'currency': null,
				'id': 'alpha',
				'inclusive': false,
				'is_percent': true,
				'metadata': {},
				'name': 'Alpha',
				'object': 'tax_rate',
				'value': 10
			}";

            var taxRate = JsonConvert.DeserializeObject<TaxRate>(json);

            Assert.True(taxRate.Name == "Alpha");
            Assert.True(taxRate.CreatedAt == 1574369950);
        }


        [Fact]
        public void TestRetrieve()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("https://api.testmode.com/tax_rates/alpha")
                .Respond("application/json", "{'id' : 'alpha', 'name' : 'Alpha'}");

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);


            var taxRateConn = conn.NewTaxRate();
            var taxRate = taxRateConn.Retrieve("alpha");

            Assert.True(taxRate.Id == "alpha");
        }


        [Fact]
        public void TestCreate()
        {
            var jsonResponse = @"{
				'created_at': 1574369950,
				'currency': null,
				'id': 'alpha',
				'inclusive': false,
				'is_percent': true,
				'metadata': {},
				'name': 'Alpha',
				'object': 'tax_rate',
				'value': 10
			}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://api.testmode.com/tax_rates")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var taxRate = conn.NewTaxRate();

            taxRate.Create();

            Assert.True(taxRate.Id == "alpha");
            Assert.True(taxRate.Value == 10);
        }

        [Fact]
        public void TestSave()
        {
            var jsonResponse = @"{
				'created_at': 1574369950,
				'currency': null,
				'id': 'alpha',
				'inclusive': false,
				'is_percent': true,
				'metadata': {},
				'name': 'Updated',
				'object': 'tax_rate',
				'value': 10
			}";


            var JsonRequest = @"{
                'name': 'Updated'
                }";

            var mockHttp = new MockHttpMessageHandler();
            var httpPatch = new HttpMethod("PATCH");
            var request = mockHttp.When(httpPatch, "https://api.testmode.com/tax_rates/alpha").WithJson(JsonRequest)
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var taxRate = CreateDefaultTaxRate(client);

            taxRate.Name = "Updated";

            taxRate.SaveAll();

            Assert.True(taxRate.Id == "alpha");
            Assert.True(taxRate.Name == "Updated");
        }

        [Fact]
        public void TestDelete()
        {
            var mockHttp = new MockHttpMessageHandler();

            var request = mockHttp.When(HttpMethod.Delete, "https://api.testmode.com/tax_rates/alpha")
                .Respond(HttpStatusCode.NoContent);

            var client = mockHttp.ToHttpClient();

            var taxRate = CreateDefaultTaxRate(client);

            taxRate.Delete();
        }


        [Fact]
        public void TestListAll()
        {
            var jsonResponseListAll = @"[{
				'created_at': 1574369950,
				'currency': null,
				'id': 'alpha',
				'inclusive': false,
				'is_percent': true,
				'metadata': {},
				'name': 'Alpha',
				'object': 'tax_rate',
				'value': 10
			}]";

            var mockHttp = new MockHttpMessageHandler();

            var mockHeader = new Dictionary<string, string>();
            mockHeader["X-Total-Count"] = "1";
            mockHeader["Link"] =
                "<https://api.sandbox.invoiced.com/tax_rates?page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/tax_rates?page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/tax_rates?page=1>; rel=\"last\"";

            var request = mockHttp.When(HttpMethod.Get, "https://api.testmode.com/tax_rates")
                .Respond(mockHeader, "application/json", jsonResponseListAll);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var taxRate = conn.NewTaxRate();

            var taxRates = taxRate.ListAll();

            Assert.True(taxRates[0].Id == "alpha");
        }
    }
}