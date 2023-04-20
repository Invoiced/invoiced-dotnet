using System.Collections.Generic;
using System.Net.Http;
using Invoiced;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace InvoicedTest
{
    public class PaymentSourceTest
    {
        private static Customer CreateDefaultCustomer(HttpClient client)
        {
            var json = @"{'id': 1234
                }";

            var customer = JsonConvert.DeserializeObject<Customer>(json);

            var connection = new Connection("voodoo", Environment.test);

            connection.TestClient(client);

            customer.ChangeConnection(connection);

            return customer;
        }

        [Fact]
        public void TestNewPaymentSourceBankAccount()
        {
            const string jsonResponse = @"{'id': 11121,'object': 'bank_account'}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://testmode/customers/1234/payment_sources")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var customer = CreateDefaultCustomer(client);
            var response = customer.CreatePaymentSource(new SourceRequest());

            Assert.True(response.Id == 11121);
            Assert.True(response.GetType() == typeof(BankAccount));
        }

        [Fact]
        public void TestNewPaymentSourceCard()
        {
            const string jsonResponse = @"{'id': 11121,'object': 'card'}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://testmode/customers/1234/payment_sources")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var customer = CreateDefaultCustomer(client);
            var response = customer.CreatePaymentSource(new SourceRequest());

            Assert.True(response.Id == 11121);
            Assert.True(response.GetType() == typeof(Card));
        }

        [Fact]
        public void TestListPaymentSources()
        {
            const string jsonResponse = @"[{'id': 11121,'object': 'card'},{'id': 11122,'object': 'bank_account'}]";

            var mockHttp = new MockHttpMessageHandler();

            var mockHeader = new Dictionary<string, string>
            {
                ["X-Total-Count"] = "2",
                ["Link"] =
                    "<https://api.sandbox.invoiced.com/customers/1234/payment_sources?page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/customers/1234/payment_sources?page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/customers/1234/payment_sources?page=1>; rel=\"last\""
            };

            mockHttp.When(HttpMethod.Get, "https://testmode/customers/1234/payment_sources")
                .Respond(mockHeader, "application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var customer = CreateDefaultCustomer(client);
            var response = customer.ListPaymentSources();

            Assert.True(response[0].Id == 11121);
            Assert.True(response[1].Id == 11122);
            Assert.True(response[0].GetType() == typeof(Card));
            Assert.True(response[1].GetType() == typeof(BankAccount));
        }
        [Fact]
        public async Task TestNewPaymentSourceBankAccountAsync()
        {
            const string jsonResponse = @"{'id': 11121,'object': 'bank_account'}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://testmode/customers/1234/payment_sources")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var customer = CreateDefaultCustomer(client);
            var response = await customer.CreatePaymentSourceAsync(new SourceRequest());

            Assert.True(response.Id == 11121);
            Assert.True(response.GetType() == typeof(BankAccount));
        }

        [Fact]
        public async Task TestNewPaymentSourceCardAsync()
        {
            const string jsonResponse = @"{'id': 11121,'object': 'card'}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://testmode/customers/1234/payment_sources")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var customer = CreateDefaultCustomer(client);
            var response = await customer.CreatePaymentSourceAsync(new SourceRequest());

            Assert.True(response.Id == 11121);
            Assert.True(response.GetType() == typeof(Card));
        }

        [Fact]
        public async Task TestListPaymentSourcesAsync()
        {
            const string jsonResponse = @"[{'id': 11121,'object': 'card'},{'id': 11122,'object': 'bank_account'}]";

            var mockHttp = new MockHttpMessageHandler();

            var mockHeader = new Dictionary<string, string>
            {
                ["X-Total-Count"] = "2",
                ["Link"] =
                    "<https://api.sandbox.invoiced.com/customers/1234/payment_sources?page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/customers/1234/payment_sources?page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/customers/1234/payment_sources?page=1>; rel=\"last\""
            };

            mockHttp.When(HttpMethod.Get, "https://testmode/customers/1234/payment_sources")
                .Respond(mockHeader, "application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var customer = CreateDefaultCustomer(client);
            var response =await customer.ListPaymentSourcesAsync();

            Assert.True(response[0].Id == 11121);
            Assert.True(response[1].Id == 11122);
            Assert.True(response[0].GetType() == typeof(Card));
            Assert.True(response[1].GetType() == typeof(BankAccount));
        }
    }
}