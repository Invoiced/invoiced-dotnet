using System;
using Xunit;
using Invoiced;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using RichardSzalay.MockHttp;
using Newtonsoft.Json;


namespace InvoicedTest
{

    public class CatalogItemTest
    {
        private static CatalogItem CreateDefaultCatalogItem(HttpClient client)
        {
            var json = @"{'id': 'alpha',
                'unit_cost': 100,
                'name': 'Alpha'
                }";

            var catalogItem = JsonConvert.DeserializeObject<CatalogItem>(json);

            var connection = new Connection("voodoo", Invoiced.Environment.test);

            connection.TestClient(client);

            catalogItem.ChangeConnection(connection);

            return catalogItem;

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
	            'object': 'catalog_item',
	            'taxable': true,
	            'taxes': [],
	            'type': null,
	            'unit_cost': 100
            }";

            var catalogItem = JsonConvert.DeserializeObject<CatalogItem>(json);

            Assert.True(catalogItem.Name == "Alpha");
            Assert.True(catalogItem.CreatedAt == 1574368157);
        }


        [Fact]
        public void TestRetrieve()
        {

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("https://testmode/catalog_items/alpha")
                .Respond("application/json", "{'id' : 'alpha', 'name' : 'Alpha'}");

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Invoiced.Environment.test);

            conn.TestClient(client);

            var catalogItemConn = conn.NewCatalogItem();

            var catalogItem = catalogItemConn.Retrieve("alpha");

            Assert.True(catalogItem.Id == "alpha");

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
	            'object': 'catalog_item',
	            'taxable': true,
	            'taxes': [],
	            'type': null,
	            'unit_cost': 100
            }";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://testmode/catalog_items")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Invoiced.Environment.test);

            conn.TestClient(client);

            var catalogItem = conn.NewCatalogItem();

            catalogItem.Create();

            Assert.True(catalogItem.Id == "alpha");

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
	            'object': 'catalog_item',
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
            var request = mockHttp.When(httpPatch, "https://testmode/catalog_items/alpha").WithJson(JsonRequest)
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var catalogItem = CreateDefaultCatalogItem(client);

            catalogItem.Name = "Updated";

            catalogItem.SaveAll();

            Assert.True(catalogItem.Id == "alpha");
            Assert.True(catalogItem.Name == "Updated");

        }

        [Fact]
        public void TestDelete()
        {

            var mockHttp = new MockHttpMessageHandler();

            var request = mockHttp.When(HttpMethod.Delete, "https://testmode/catalog_items/alpha")
                .Respond(HttpStatusCode.NoContent);

            var client = mockHttp.ToHttpClient();

            var catalogItem = CreateDefaultCatalogItem(client);

            catalogItem.Delete();

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
	            'object': 'catalog_item',
	            'taxable': true,
	            'taxes': [],
	            'type': null,
	            'unit_cost': 100
            }]";

            var mockHttp = new MockHttpMessageHandler();

            var mockHeader = new Dictionary<string, string>();
            mockHeader["X-Total-Count"] = "1";
            mockHeader["Link"] =
                "<https://api.sandbox.invoiced.com/catalog_items?page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/catalog_items?page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/catalog_items?page=1>; rel=\"last\"";

            var request = mockHttp.When(HttpMethod.Get, "https://testmode/catalog_items")
	            .Respond(mockHeader, "application/json", jsonResponseListAll);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Invoiced.Environment.test);

            conn.TestClient(client);

            var catalogItem = conn.NewCatalogItem();

            var catalogItems = catalogItem.ListAll();

            Assert.True(catalogItems[0].Id == "alpha");

        }

    }

}
