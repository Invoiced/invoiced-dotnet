using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Invoiced;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Xunit;

namespace InvoicedTest
{
    public class ItemTest
    {
        private static Item CreateDefaultItem(HttpClient client)
        {
            var json = @"{'id': 'alpha',
                'unit_cost': 100,
                'name': 'Alpha'
                }";

            var item = JsonConvert.DeserializeObject<Item>(json);

            var connection = new Connection("voodoo", Environment.test);

            connection.TestClient(client);

            item.ChangeConnection(connection);

            return item;
        }

        [Fact]
        public void TestDeserialize()
        {
            var json = @"{
	            'avalara_tax_code': null,
	            'created_at': 1574368157,
	            'currency': 'usd',
	            'description': '',
	            'discountable': true,
	            'gl_account': null,
	            'id': 'alpha',
	            'metadata': {},
	            'name': 'Alpha',
	            'object': 'item',
	            'taxable': true,
	            'taxes': [],
	            'type': null,
	            'unit_cost': 100
            }";

            var item = JsonConvert.DeserializeObject<Item>(json);

            Assert.True(item.Name == "Alpha");
            Assert.True(item.CreatedAt == 1574368157);
        }


        [Fact]
        public void TestRetrieve()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("https://api.testmode.com/items/alpha")
                .Respond("application/json", "{'id' : 'alpha', 'name' : 'Alpha'}");

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var itemConn = conn.NewItem();

            var item = itemConn.Retrieve("alpha");

            Assert.True(item.Id == "alpha");
        }


        [Fact]
        public void TestCreate()
        {
            var jsonResponse = @"{
	            'avalara_tax_code': null,
	            'created_at': 1574368157,
	            'currency': 'usd',
	            'description': '',
	            'discountable': true,
	            'gl_account': null,
	            'id': 'alpha',
	            'metadata': {},
	            'name': 'Alpha',
	            'object': 'item',
	            'taxable': true,
	            'taxes': [],
	            'type': null,
	            'unit_cost': 100
            }";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://api.testmode.com/items")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var item = conn.NewItem();

            item.Create();

            Assert.True(item.Id == "alpha");
        }

        [Fact]
        public void TestSave()
        {
            var jsonResponse = @"{
	            'avalara_tax_code': null,
	            'created_at': 1574368157,
	            'currency': 'usd',
	            'description': '',
	            'discountable': true,
	            'gl_account': null,
	            'id': 'alpha',
	            'metadata': {},
	            'name': 'Updated',
	            'object': 'item',
	            'taxable': true,
	            'taxes': [],
	            'type': null,
	            'unit_cost': 100
            }";


            var JsonRequest = @"{
                'name': 'Updated'
                }";

            var mockHttp = new MockHttpMessageHandler();
            var httpPatch = new HttpMethod("PATCH");
            var request = mockHttp.When(httpPatch, "https://api.testmode.com/items/alpha").WithJson(JsonRequest)
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var item = CreateDefaultItem(client);

            item.Name = "Updated";

            item.SaveAll();

            Assert.True(item.Id == "alpha");
            Assert.True(item.Name == "Updated");
        }

        [Fact]
        public void TestDelete()
        {
            var mockHttp = new MockHttpMessageHandler();

            var request = mockHttp.When(HttpMethod.Delete, "https://api.testmode.com/items/alpha")
                .Respond(HttpStatusCode.NoContent);

            var client = mockHttp.ToHttpClient();

            var item = CreateDefaultItem(client);

            item.Delete();
        }


        [Fact]
        public void TestListAll()
        {
            var jsonResponseListAll = @"[{
	            'avalara_tax_code': null,
	            'created_at': 1574368157,
	            'currency': 'usd',
	            'description': '',
	            'discountable': true,
	            'gl_account': null,
	            'id': 'alpha',
	            'metadata': {},
	            'name': 'Updated',
	            'object': 'item',
	            'taxable': true,
	            'taxes': [],
	            'type': null,
	            'unit_cost': 100
            }]";

            var mockHttp = new MockHttpMessageHandler();

            var mockHeader = new Dictionary<string, string>();
            mockHeader["X-Total-Count"] = "1";
            mockHeader["Link"] =
                "<https://api.sandbox.invoiced.com/items?page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/items?page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/items?page=1>; rel=\"last\"";

            var request = mockHttp.When(HttpMethod.Get, "https://api.testmode.com/items")
                .Respond(mockHeader, "application/json", jsonResponseListAll);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var item = conn.NewItem();

            var items = item.ListAll();

            Assert.True(items[0].Id == "alpha");
        }
    }
}