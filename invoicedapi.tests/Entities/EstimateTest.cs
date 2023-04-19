using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Invoiced;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Xunit;

namespace InvoicedTest
{
    public class EstimateTest
    {
        private static Estimate CreateDefaultEstimate(HttpClient client)
        {
            var json = @"{'id': 11658 }";

            var estimate = JsonConvert.DeserializeObject<Estimate>(json);

            var connection = new Connection("voodoo", Environment.test);

            connection.TestClient(client);

            estimate.ChangeConnection(connection);

            return estimate;
        }

        [Fact]
        public void TestDeserialize()
        {
            var json = @"{
				'approval': null,
				'approved': null,
				'closed': false,
				'created_at': 1574371488,
				'currency': 'usd',
				'customer': 594287,
				'date': 1574371480,
				'deposit': 0,
				'deposit_paid': false,
				'discounts': [],
				'draft': false,
				'expiration_date': null,
				'id': 11658,
				'invoice': null,
				'items': [],
				'metadata': {},
				'name': 'Estimate',
				'notes': null,
				'number': 'EST-00003',
				'object': 'estimate',
				'payment_terms': 'NET 7',
				'pdf_url': 'https://ajwt.sandbox.invoiced.com/estimates/jrjtqYLyONCu51cocXVpIpcv/pdf',
				'purchase_order': null,
				'ship_to': null,
				'shipping': [],
				'status': 'not_sent',
				'subtotal': 0,
				'taxes': [],
				'total': 0,
				'url': 'https://ajwt.sandbox.invoiced.com/estimates/jrjtqYLyONCu51cocXVpIpcv'
			}";

            var estimate = JsonConvert.DeserializeObject<Estimate>(json);

            Assert.True(estimate.Id == 11658);
            Assert.True(estimate.Name == "Estimate");
            Assert.True(estimate.Subtotal == 0);
        }


        [Fact]
        public void TestRetrieve()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("https://testmode/estimates/4")
                .Respond("application/json", "{'id' : 4, 'number' : 'EST-0001'}");

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var estimateConn = conn.NewEstimate();

            var estimate = estimateConn.Retrieve(4);

            Assert.True(estimate.Number == "EST-0001");
        }
        [Fact]
        public async Task TestRetrieveAsync()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("https://testmode/estimates/4")
                .Respond("application/json", "{'id' : 4, 'number' : 'EST-0001'}");

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var estimateConn = conn.NewEstimate();

            var estimate = await estimateConn.RetrieveAsync(4);

            Assert.True(estimate.Number == "EST-0001");
        }


        [Fact]
        public void TestCreate()
        {
            var jsonResponse = @"{
				'approval': null,
				'approved': null,
				'closed': false,
				'created_at': 1574371488,
				'currency': 'usd',
				'customer': 594287,
				'date': 1574371480,
				'deposit': 0,
				'deposit_paid': false,
				'discounts': [],
				'draft': false,
				'expiration_date': null,
				'id': 11658,
				'invoice': null,
				'items': [],
				'metadata': {},
				'name': 'Estimate',
				'notes': null,
				'number': 'EST-00003',
				'object': 'estimate',
				'payment_terms': 'NET 7',
				'pdf_url': 'https://ajwt.sandbox.invoiced.com/estimates/jrjtqYLyONCu51cocXVpIpcv/pdf',
				'purchase_order': null,
				'ship_to': null,
				'shipping': [],
				'status': 'not_sent',
				'subtotal': 0,
				'taxes': [],
				'total': 0,
				'url': 'https://ajwt.sandbox.invoiced.com/estimates/jrjtqYLyONCu51cocXVpIpcv'
			}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://testmode/estimates").Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var estimate = conn.NewEstimate();

            estimate.Create();

            Assert.True(estimate.Id == 11658);
        }
        [Fact]
        public async Task TestCreateAsync()
        {
            var jsonResponse = @"{
				'approval': null,
				'approved': null,
				'closed': false,
				'created_at': 1574371488,
				'currency': 'usd',
				'customer': 594287,
				'date': 1574371480,
				'deposit': 0,
				'deposit_paid': false,
				'discounts': [],
				'draft': false,
				'expiration_date': null,
				'id': 11658,
				'invoice': null,
				'items': [],
				'metadata': {},
				'name': 'Estimate',
				'notes': null,
				'number': 'EST-00003',
				'object': 'estimate',
				'payment_terms': 'NET 7',
				'pdf_url': 'https://ajwt.sandbox.invoiced.com/estimates/jrjtqYLyONCu51cocXVpIpcv/pdf',
				'purchase_order': null,
				'ship_to': null,
				'shipping': [],
				'status': 'not_sent',
				'subtotal': 0,
				'taxes': [],
				'total': 0,
				'url': 'https://ajwt.sandbox.invoiced.com/estimates/jrjtqYLyONCu51cocXVpIpcv'
			}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://testmode/estimates").Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var estimate = conn.NewEstimate();

            await estimate.CreateAsync();

            Assert.True(estimate.Id == 11658);
        }

        [Fact]
        public void TestSave()
        {
            var jsonResponse = @"{
				'approval': null,
				'approved': null,
				'closed': false,
				'created_at': 1574371488,
				'currency': 'usd',
				'customer': 594287,
				'date': 1574371480,
				'deposit': 0,
				'deposit_paid': false,
				'discounts': [],
				'draft': false,
				'expiration_date': null,
				'id': 11658,
				'invoice': null,
				'items': [],
				'metadata': {},
				'name': 'Updated',
				'notes': null,
				'number': 'EST-00003',
				'object': 'estimate',
				'payment_terms': 'NET 7',
				'pdf_url': 'https://ajwt.sandbox.invoiced.com/estimates/jrjtqYLyONCu51cocXVpIpcv/pdf',
				'purchase_order': null,
				'ship_to': null,
				'shipping': [],
				'status': 'not_sent',
				'subtotal': 0,
				'taxes': [],
				'total': 0,
				'url': 'https://ajwt.sandbox.invoiced.com/estimates/jrjtqYLyONCu51cocXVpIpcv'
			}";

            var mockHttp = new MockHttpMessageHandler();
            var httpPatch = new HttpMethod("PATCH");
            var request = mockHttp.When(httpPatch, "https://testmode/estimates/11658")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var estimate = CreateDefaultEstimate(client);

            estimate.Name = "Updated";

            estimate.SaveAll();

            Assert.True(estimate.Name == "Updated");
        }
        [Fact]
        public async Task TestSaveAsync()
        {
            var jsonResponse = @"{
				'approval': null,
				'approved': null,
				'closed': false,
				'created_at': 1574371488,
				'currency': 'usd',
				'customer': 594287,
				'date': 1574371480,
				'deposit': 0,
				'deposit_paid': false,
				'discounts': [],
				'draft': false,
				'expiration_date': null,
				'id': 11658,
				'invoice': null,
				'items': [],
				'metadata': {},
				'name': 'Updated',
				'notes': null,
				'number': 'EST-00003',
				'object': 'estimate',
				'payment_terms': 'NET 7',
				'pdf_url': 'https://ajwt.sandbox.invoiced.com/estimates/jrjtqYLyONCu51cocXVpIpcv/pdf',
				'purchase_order': null,
				'ship_to': null,
				'shipping': [],
				'status': 'not_sent',
				'subtotal': 0,
				'taxes': [],
				'total': 0,
				'url': 'https://ajwt.sandbox.invoiced.com/estimates/jrjtqYLyONCu51cocXVpIpcv'
			}";

            var mockHttp = new MockHttpMessageHandler();
            var httpPatch = new HttpMethod("PATCH");
            var request = mockHttp.When(httpPatch, "https://testmode/estimates/11658")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var estimate = CreateDefaultEstimate(client);

            estimate.Name = "Updated";

            await estimate.SaveAllAsync();

            Assert.True(estimate.Name == "Updated");
        }

        [Fact]
        public void TestDelete()
        {
            var mockHttp = new MockHttpMessageHandler();

            var request = mockHttp.When(HttpMethod.Delete, "https://testmode/estimates/11658")
                .Respond(HttpStatusCode.NoContent);

            var client = mockHttp.ToHttpClient();

            var estimate = CreateDefaultEstimate(client);

            estimate.Delete();
        }
        [Fact]
        public async Task TestDeleteAsync()
        {
            var mockHttp = new MockHttpMessageHandler();

            var request = mockHttp.When(HttpMethod.Delete, "https://testmode/estimates/11658")
                .Respond(HttpStatusCode.NoContent);

            var client = mockHttp.ToHttpClient();

            var estimate = CreateDefaultEstimate(client);

           await estimate.DeleteAsync();
        }


        [Fact]
        public void TestListAll()
        {
            var jsonResponseListAll = @"[{
				'approval': null,
				'approved': null,
				'closed': false,
				'created_at': 1574371488,
				'currency': 'usd',
				'customer': 594287,
				'date': 1574371480,
				'deposit': 0,
				'deposit_paid': false,
				'discounts': [],
				'draft': false,
				'expiration_date': null,
				'id': 11658,
				'invoice': null,
				'items': [],
				'metadata': {},
				'name': 'Estimate',
				'notes': null,
				'number': 'EST-00003',
				'object': 'estimate',
				'payment_terms': 'NET 7',
				'pdf_url': 'https://ajwt.sandbox.invoiced.com/estimates/jrjtqYLyONCu51cocXVpIpcv/pdf',
				'purchase_order': null,
				'ship_to': null,
				'shipping': [],
				'status': 'not_sent',
				'subtotal': 0,
				'taxes': [],
				'total': 0,
				'url': 'https://ajwt.sandbox.invoiced.com/estimates/jrjtqYLyONCu51cocXVpIpcv'
			}]";

            var mockHttp = new MockHttpMessageHandler();

            var mockHeader = new Dictionary<string, string>();
            mockHeader["X-Total-Count"] = "1";
            mockHeader["Link"] =
                "<https://api.sandbox.invoiced.com/estimates?page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/estimates?page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/estimates?page=1>; rel=\"last\"";

            var request = mockHttp.When(HttpMethod.Get, "https://testmode/estimates")
                .Respond(mockHeader, "application/json", jsonResponseListAll);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var estimate = conn.NewEstimate();

            var estimates = estimate.ListAll();

            Assert.True(estimates[0].Id == 11658);
            Assert.True(estimates[0].Total == 0);
        }
        [Fact]
        public async Task TestListAllAsync()
        {
            var jsonResponseListAll = @"[{
				'approval': null,
				'approved': null,
				'closed': false,
				'created_at': 1574371488,
				'currency': 'usd',
				'customer': 594287,
				'date': 1574371480,
				'deposit': 0,
				'deposit_paid': false,
				'discounts': [],
				'draft': false,
				'expiration_date': null,
				'id': 11658,
				'invoice': null,
				'items': [],
				'metadata': {},
				'name': 'Estimate',
				'notes': null,
				'number': 'EST-00003',
				'object': 'estimate',
				'payment_terms': 'NET 7',
				'pdf_url': 'https://ajwt.sandbox.invoiced.com/estimates/jrjtqYLyONCu51cocXVpIpcv/pdf',
				'purchase_order': null,
				'ship_to': null,
				'shipping': [],
				'status': 'not_sent',
				'subtotal': 0,
				'taxes': [],
				'total': 0,
				'url': 'https://ajwt.sandbox.invoiced.com/estimates/jrjtqYLyONCu51cocXVpIpcv'
			}]";

            var mockHttp = new MockHttpMessageHandler();

            var mockHeader = new Dictionary<string, string>();
            mockHeader["X-Total-Count"] = "1";
            mockHeader["Link"] =
                "<https://api.sandbox.invoiced.com/estimates?page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/estimates?page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/estimates?page=1>; rel=\"last\"";

            var request = mockHttp.When(HttpMethod.Get, "https://testmode/estimates")
                .Respond(mockHeader, "application/json", jsonResponseListAll);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var estimate = conn.NewEstimate();

            var estimates = await estimate.ListAllAsync();

            Assert.True(estimates[0].Id == 11658);
            Assert.True(estimates[0].Total == 0);
        }

        [Fact]
        public void TestConvertToInvoice()
        {
            var jsonResponse = @"{
				'attempt_count': 0,
				'autopay': false,
				'balance': 0,
				'chase': false,
				'closed': true,
				'created_at': 1574371986,
				'csv_url': 'https://ajwt.sandbox.invoiced.com/invoices/BZ7mtbqMCKuAq0rgKfzWGZeg/csv',
				'currency': 'usd',
				'customer': 594287,
				'date': 1574371986,
				'discounts': [],
				'draft': false,
				'due_date': 1574976786,
				'id': 2356229,
				'items': [],
				'metadata': {},
				'name': 'Invoice',
				'needs_attention': false,
				'next_chase_on': null,
				'next_payment_attempt': null,
				'notes': null,
				'number': 'INV-000013',
				'object': 'invoice',
				'paid': true,
				'payment_plan': null,
				'payment_source': null,
				'payment_terms': 'NET 7',
				'payment_url': null,
				'pdf_url': 'https://ajwt.sandbox.invoiced.com/invoices/BZ7mtbqMCKuAq0rgKfzWGZeg/pdf',
				'purchase_order': null,
				'ship_to': null,
				'shipping': [],
				'status': 'paid',
				'subscription': null,
				'subtotal': 0,
				'taxes': [],
				'total': 0,
				'url': 'https://ajwt.sandbox.invoiced.com/invoices/BZ7mtbqMCKuAq0rgKfzWGZeg'
			}";

            var mockHttp = new MockHttpMessageHandler();
            var request = mockHttp.When(HttpMethod.Post, "https://testmode/estimates/11658/invoice")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var estimate = CreateDefaultEstimate(client);

            var invoice = estimate.ConvertToInvoice();

            Assert.True(invoice.CreatedAt == 1574371986);
        }
        [Fact]
        public async Task TestConvertToInvoiceAsync()
        {
            var jsonResponse = @"{
				'attempt_count': 0,
				'autopay': false,
				'balance': 0,
				'chase': false,
				'closed': true,
				'created_at': 1574371986,
				'csv_url': 'https://ajwt.sandbox.invoiced.com/invoices/BZ7mtbqMCKuAq0rgKfzWGZeg/csv',
				'currency': 'usd',
				'customer': 594287,
				'date': 1574371986,
				'discounts': [],
				'draft': false,
				'due_date': 1574976786,
				'id': 2356229,
				'items': [],
				'metadata': {},
				'name': 'Invoice',
				'needs_attention': false,
				'next_chase_on': null,
				'next_payment_attempt': null,
				'notes': null,
				'number': 'INV-000013',
				'object': 'invoice',
				'paid': true,
				'payment_plan': null,
				'payment_source': null,
				'payment_terms': 'NET 7',
				'payment_url': null,
				'pdf_url': 'https://ajwt.sandbox.invoiced.com/invoices/BZ7mtbqMCKuAq0rgKfzWGZeg/pdf',
				'purchase_order': null,
				'ship_to': null,
				'shipping': [],
				'status': 'paid',
				'subscription': null,
				'subtotal': 0,
				'taxes': [],
				'total': 0,
				'url': 'https://ajwt.sandbox.invoiced.com/invoices/BZ7mtbqMCKuAq0rgKfzWGZeg'
			}";

            var mockHttp = new MockHttpMessageHandler();
            var request = mockHttp.When(HttpMethod.Post, "https://testmode/estimates/11658/invoice")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var estimate = CreateDefaultEstimate(client);

            var invoice = await estimate.ConvertToInvoiceAsync();

            Assert.True(invoice.CreatedAt == 1574371986);
        }
    }
}