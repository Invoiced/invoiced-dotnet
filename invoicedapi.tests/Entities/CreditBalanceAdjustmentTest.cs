using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Invoiced;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace InvoicedTest
{
    public class CreditBalanceAdjustmentTest
    {
        private static CreditBalanceAdjustment CreateDefaultCreditBalanceAdjustment(HttpClient client)
        {
            var json = @"{'id': 1234}";

            var creditBalanceAdjustment = JsonConvert.DeserializeObject<CreditBalanceAdjustment>(json);

            var connection = new Connection("voodoo", Environment.test);

            connection.TestClient(client);

            creditBalanceAdjustment.ChangeConnection(connection);

            return creditBalanceAdjustment;
        }

        [Fact]
        public void TestDeserialize()
        {
            var json = @"{
				'amount': 50,
	            'created_at': 1607550710,
	            'currency': 'usd',
	            'customer': 78,
	            'date': 1607550710,
	            'id': 717,
	            'notes': null,
	            'object': 'credit_balance_adjustment'
	        }";

            var creditBalanceAdjustment = JsonConvert.DeserializeObject<CreditBalanceAdjustment>(json);

            Assert.True(creditBalanceAdjustment.Amount == 50.0);
            Assert.True(creditBalanceAdjustment.Customer == 78);
            Assert.True(creditBalanceAdjustment.Currency == "usd");
            Assert.True(creditBalanceAdjustment.CreatedAt == 1607550710);
            Assert.True(creditBalanceAdjustment.Date == 1607550710);
            Assert.True(creditBalanceAdjustment.Id == 717);
            Assert.True(creditBalanceAdjustment.Notes == null);
        }


        [Fact]
        public void TestRetrieve()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("https://testmode/credit_balance_adjustments/1234")
                .Respond("application/json", "{'id' : '1234', 'notes' : 'Test'}");

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var creditBalanceAdjustmentConn = conn.NewCreditBalanceAdjustment();

            var creditBalanceAdjustment = creditBalanceAdjustmentConn.Retrieve(1234);

            Assert.True(creditBalanceAdjustment.Id == 1234);
            Assert.True(creditBalanceAdjustment.Notes == "Test");
        }
        [Fact]
        public async Task TestRetrieveAsync()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("https://testmode/credit_balance_adjustments/1234")
                .Respond("application/json", "{'id' : '1234', 'notes' : 'Test'}");

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var creditBalanceAdjustmentConn = conn.NewCreditBalanceAdjustment();

            var creditBalanceAdjustment = await creditBalanceAdjustmentConn.RetrieveAsync(1234);

            Assert.True(creditBalanceAdjustment.Id == 1234);
            Assert.True(creditBalanceAdjustment.Notes == "Test");
        }


        [Fact]
        public void TestCreate()
        {
	        var jsonResponse = @"{
				'amount': 50,
	            'created_at': 1607550710,
	            'currency': 'usd',
	            'customer': 78,
	            'date': 1607550710,
	            'id': 717,
	            'notes': null,
	            'object': 'credit_balance_adjustment'
	        }";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://testmode/credit_balance_adjustments")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var creditBalanceAdjustment = conn.NewCreditBalanceAdjustment();

            creditBalanceAdjustment.Create();

            Assert.True(creditBalanceAdjustment.Id == 717);
        }
        [Fact]
        public async Task TestCreateAsync()
        {
	        var jsonResponse = @"{
				'amount': 50,
	            'created_at': 1607550710,
	            'currency': 'usd',
	            'customer': 78,
	            'date': 1607550710,
	            'id': 717,
	            'notes': null,
	            'object': 'credit_balance_adjustment'
	        }";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://testmode/credit_balance_adjustments")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var creditBalanceAdjustment = conn.NewCreditBalanceAdjustment();

            await creditBalanceAdjustment.CreateAsync();

            Assert.True(creditBalanceAdjustment.Id == 717);
        }

        [Fact]
        public void TestSave()
        {
	        var jsonRequest = @"{
				'notes': 'Updated'
			}";

	        var jsonResponse = @"{
				'amount': 50,
	            'created_at': 1607550710,
	            'currency': 'usd',
	            'customer': 78,
	            'date': 1607550710,
	            'id': 717,
	            'notes': 'Updated',
	            'object': 'credit_balance_adjustment'
	        }";

	        var mockHttp = new MockHttpMessageHandler();
            var httpPatch = new HttpMethod("PATCH");
            var request = mockHttp.When(httpPatch, "https://testmode/credit_balance_adjustments/1234")
	            .WithJson(jsonRequest)
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var creditBalanceAdjustment = CreateDefaultCreditBalanceAdjustment(client);

            creditBalanceAdjustment.Notes = "Updated";

            creditBalanceAdjustment.SaveAll();

            Assert.True(creditBalanceAdjustment.Id == 717);
            Assert.True(creditBalanceAdjustment.Notes == "Updated");
        }
        
        [Fact]
        public async Task TestSaveAsync()
        {
	        var jsonRequest = @"{
				'notes': 'Updated'
			}";

	        var jsonResponse = @"{
				'amount': 50,
	            'created_at': 1607550710,
	            'currency': 'usd',
	            'customer': 78,
	            'date': 1607550710,
	            'id': 717,
	            'notes': 'Updated',
	            'object': 'credit_balance_adjustment'
	        }";

	        var mockHttp = new MockHttpMessageHandler();
            var httpPatch = new HttpMethod("PATCH");
            var request = mockHttp.When(httpPatch, "https://testmode/credit_balance_adjustments/1234")
	            .WithJson(jsonRequest)
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var creditBalanceAdjustment = CreateDefaultCreditBalanceAdjustment(client);

            creditBalanceAdjustment.Notes = "Updated";

            await creditBalanceAdjustment.SaveAllAsync();

            Assert.True(creditBalanceAdjustment.Id == 717);
            Assert.True(creditBalanceAdjustment.Notes == "Updated");
        }

        [Fact]
        public void TestDelete()
        {
            var mockHttp = new MockHttpMessageHandler();

            var request = mockHttp.When(HttpMethod.Delete, "https://testmode/credit_balance_adjustments/1234")
                .Respond(HttpStatusCode.NoContent);

            var client = mockHttp.ToHttpClient();

            var creditBalanceAdjustment = CreateDefaultCreditBalanceAdjustment(client);

            creditBalanceAdjustment.Delete();
        }
        [Fact]
        public async Task TestDeleteAsync()
        {
            var mockHttp = new MockHttpMessageHandler();

            var request = mockHttp.When(HttpMethod.Delete, "https://testmode/credit_balance_adjustments/1234")
                .Respond(HttpStatusCode.NoContent);

            var client = mockHttp.ToHttpClient();

            var creditBalanceAdjustment = CreateDefaultCreditBalanceAdjustment(client);

            await creditBalanceAdjustment.DeleteAsync();
        }


        [Fact]
        public void TestListAll()
        {
	        var jsonResponse = @"[{
				'amount': 50,
	            'created_at': 1607550710,
	            'currency': 'usd',
	            'customer': 78,
	            'date': 1607550710,
	            'id': 717,
	            'notes': 'Updated',
	            'object': 'credit_balance_adjustment'
	        }]";

            var mockHttp = new MockHttpMessageHandler();

            var mockHeader = new Dictionary<string, string>();
            mockHeader["X-Total-Count"] = "1";
            mockHeader["Link"] =
                "<https://api.sandbox.invoiced.com/credit_balance_adjustments?page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/credit_balance_adjustments?page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/credit_balance_adjustments?page=1>; rel=\"last\"";

            var request = mockHttp.When(HttpMethod.Get, "https://testmode/credit_balance_adjustments")
                .Respond(mockHeader, "application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var creditBalanceAdjustment = conn.NewCreditBalanceAdjustment();

            var creditBalanceAdjustments = creditBalanceAdjustment.ListAll();

            Assert.True(creditBalanceAdjustments[0].Id == 717);
        }
        
        [Fact]
        public async Task TestListAllAsync()
        {
	        var jsonResponse = @"[{
				'amount': 50,
	            'created_at': 1607550710,
	            'currency': 'usd',
	            'customer': 78,
	            'date': 1607550710,
	            'id': 717,
	            'notes': 'Updated',
	            'object': 'credit_balance_adjustment'
	        }]";

            var mockHttp = new MockHttpMessageHandler();

            var mockHeader = new Dictionary<string, string>();
            mockHeader["X-Total-Count"] = "1";
            mockHeader["Link"] =
                "<https://api.sandbox.invoiced.com/credit_balance_adjustments?page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/credit_balance_adjustments?page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/credit_balance_adjustments?page=1>; rel=\"last\"";

            var request = mockHttp.When(HttpMethod.Get, "https://testmode/credit_balance_adjustments")
                .Respond(mockHeader, "application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var creditBalanceAdjustment = conn.NewCreditBalanceAdjustment();

            var creditBalanceAdjustments = await creditBalanceAdjustment.ListAllAsync();

            Assert.True(creditBalanceAdjustments[0].Id == 717);
        }
    }
}