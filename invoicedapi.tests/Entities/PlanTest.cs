using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Invoiced;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Xunit;

namespace InvoicedTest
{
    public class PlanTest
    {
        private static Plan CreateDefaultPlan(HttpClient client)
        {
            var json = @"{'id': 'alpha'}";

            var plan = JsonConvert.DeserializeObject<Plan>(json);

            var connection = new Connection("voodoo", Environment.test);

            connection.TestClient(client);

            plan.ChangeConnection(connection);

            return plan;
        }

        [Fact]
        public void TestDeserialize()
        {
            var json = @"{
				'amount': 100,
				'catalog_item': null,
				'created_at': 1574369297,
				'currency': 'usd',
				'description': null,
				'id': 'alpha',
				'interval': 'month',
				'interval_count': 1,
				'metadata': {},
				'name': 'Alpha',
				'notes': null,
				'object': 'plan',
				'pricing_mode': 'per_unit',
				'quantity_type': 'constant',
				'tiers': null
			}";

            var plan = JsonConvert.DeserializeObject<Plan>(json);

            Assert.True(plan.Name == "Alpha");
            Assert.True(plan.CreatedAt == 1574369297);
        }


        [Fact]
        public void TestRetrieve()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("https://testmode/plans/alpha")
                .Respond("application/json", "{'id' : 'alpha', 'name' : 'Alpha'}");

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var planConn = conn.NewPlan();

            var plan = planConn.Retrieve("alpha");

            Assert.True(plan.Id == "alpha");
        }


        [Fact]
        public void TestCreate()
        {
            var jsonResponse = @"{
				'amount': 100,
				'catalog_item': null,
				'created_at': 1574369297,
				'currency': 'usd',
				'description': null,
				'id': 'alpha',
				'interval': 'month',
				'interval_count': 1,
				'metadata': {},
				'name': 'Alpha',
				'notes': null,
				'object': 'plan',
				'pricing_mode': 'per_unit',
				'quantity_type': 'constant',
				'tiers': null
			}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://testmode/plans")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var plan = conn.NewPlan();

            plan.Create();

            Assert.True(plan.Id == "alpha");
            Assert.True(plan.PricingMode == "per_unit");
        }

        [Fact]
        public void TestSave()
        {
            var jsonResponse = @"{
				'amount': 100,
				'catalog_item': null,
				'created_at': 1574369297,
				'currency': 'usd',
				'description': null,
				'id': 'alpha',
				'interval': 'month',
				'interval_count': 1,
				'metadata': {},
				'name': 'Updated',
				'notes': null,
				'object': 'plan',
				'pricing_mode': 'per_unit',
				'quantity_type': 'constant',
				'tiers': null
			}";


            var JsonRequest = @"{
                'name': 'Updated'
                }";

            var mockHttp = new MockHttpMessageHandler();
            var httpPatch = new HttpMethod("PATCH");
            var request = mockHttp.When(httpPatch, "https://testmode/plans/alpha").WithJson(JsonRequest)
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var plan = CreateDefaultPlan(client);

            plan.Name = "Updated";

            plan.SaveAll();

            Assert.True(plan.Id == "alpha");
            Assert.True(plan.Name == "Updated");
        }

        [Fact]
        public void TestDelete()
        {
            var mockHttp = new MockHttpMessageHandler();

            var request = mockHttp.When(HttpMethod.Delete, "https://testmode/plans/alpha")
                .Respond(HttpStatusCode.NoContent);

            var client = mockHttp.ToHttpClient();

            var plan = CreateDefaultPlan(client);

            plan.Delete();
        }


        [Fact]
        public void TestListAll()
        {
            var jsonResponseListAll = @"[{
				'amount': 100,
				'catalog_item': null,
				'created_at': 1574369297,
				'currency': 'usd',
				'description': null,
				'id': 'alpha',
				'interval': 'month',
				'interval_count': 1,
				'metadata': {},
				'name': 'Alpha',
				'notes': null,
				'object': 'plan',
				'pricing_mode': 'per_unit',
				'quantity_type': 'constant',
				'tiers': null
			}]";

            var mockHttp = new MockHttpMessageHandler();

            var mockHeader = new Dictionary<string, string>();
            mockHeader["X-Total-Count"] = "1";
            mockHeader["Link"] =
                "<https://api.sandbox.invoiced.com/plans?page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/plans?page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/plans?page=1>; rel=\"last\"";

            var request = mockHttp.When(HttpMethod.Get, "https://testmode/plans")
                .Respond(mockHeader, "application/json", jsonResponseListAll);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var plan = conn.NewPlan();

            var plans = plan.ListAll();

            Assert.True(plans[0].Id == "alpha");
        }
    }
}