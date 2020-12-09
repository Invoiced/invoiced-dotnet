using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Invoiced;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Xunit;

namespace InvoicedTest
{
    public class PaymentTest
    {
        public static Payment CreateDefaultPayment(HttpClient client)
        {
            var json = @"{'id': 656093
                }";

            var payment = JsonConvert.DeserializeObject<Payment>(json);

            var connection = new Connection("voodoo", Environment.test);

            connection.TestClient(client);

            payment.ChangeConnection(connection);

            return payment;
        }

        [Fact]
        public void TestDeserialize()
        {
            var json = @"{
				'amount': 9999,
				'created_at': 1574374740,
				'credit_note': null,
				'currency': 'usd',
				'customer': 594287,
				'date': 1574374740,
				'gateway': 'test',
				'gateway_id': '5dd70d54b17eb',
				'id': 656093,
				'invoice': 2356243,
				'metadata': {},
				'method': 'credit_card',
				'notes': null,
				'object': 'payment',
				'parent_payment': null,
				'payment_source': {
					'brand': 'Visa',
					'chargeable': true,
					'created_at': 1574276914,
					'exp_month': 12,
					'exp_year': 2030,
					'failure_reason': null,
					'funding': 'credit',
					'gateway': 'test',
					'gateway_customer': null,
					'gateway_id': '5dd58f32624a4',
					'id': 7980,
					'last4': '4242',
					'merchant_account': null,
					'receipt_email': 'example@example.com',
					'object': 'card'
				},
				'pdf_url': 'https://ajwt.sandbox.invoiced.com/payments/LyGsrTkfXu2QN9w7stT2BYvX/pdf',
				'status': 'succeeded',
				'type': 'charge'
			}";

            var payment = JsonConvert.DeserializeObject<Payment>(json);

            Assert.True(payment.Id == 656093);
            Assert.True(payment.Status == "succeeded");
        }

        [Fact]
        public void TestRetrieve()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("https://testmode/payments/656093")
                .Respond("application/json", "{'id' : 656093, 'currency': 'usd'}");

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var paymentConn = conn.NewPayment();

            var payment = paymentConn.Retrieve(656093);

            Assert.True(payment.Currency == "usd");
        }


        [Fact]
        public void TestCreate()
        {
            var jsonResponse = @"{
				'amount': 9999,
				'created_at': 1574374740,
				'credit_note': null,
				'currency': 'usd',
				'customer': 594287,
				'date': 1574374740,
				'gateway': 'test',
				'gateway_id': '5dd70d54b17eb',
				'id': 656093,
				'invoice': 2356243,
				'metadata': {},
				'method': 'credit_card',
				'notes': null,
				'object': 'payment',
				'parent_payment': null,
				'payment_source': {
					'brand': 'Visa',
					'chargeable': true,
					'created_at': 1574276914,
					'exp_month': 12,
					'exp_year': 2030,
					'failure_reason': null,
					'funding': 'credit',
					'gateway': 'test',
					'gateway_customer': null,
					'gateway_id': '5dd58f32624a4',
					'id': 7980,
					'last4': '4242',
					'merchant_account': null,
					'receipt_email': 'example@example.com',
					'object': 'card'
				},
				'pdf_url': 'https://ajwt.sandbox.invoiced.com/payments/LyGsrTkfXu2QN9w7stT2BYvX/pdf',
				'status': 'succeeded',
				'type': 'charge'
			}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://testmode/payments")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var payment = conn.NewPayment();
            payment.Amount = 100;
            payment.AppliedTo = new[]
            {
	            new PaymentItem {Type = "invoice", Amount = 100, Invoice = 1234}
            };

            payment.Create();

            Assert.True(payment.Id == 656093);
            Assert.True(payment.Method == "credit_card");
        }

        [Fact]
        public void TestSave()
        {
            var jsonResponse = @"{
				'amount': 9999,
				'created_at': 1574374740,
				'credit_note': null,
				'currency': 'usd',
				'customer': 594287,
				'date': 1574374740,
				'gateway': 'test',
				'gateway_id': '5dd70d54b17eb',
				'id': 656093,
				'invoice': 2356243,
				'metadata': {},
				'method': 'credit_card',
				'notes': 'example',
				'object': 'payment',
				'parent_payment': null,
				'payment_source': {
					'brand': 'Visa',
					'chargeable': true,
					'created_at': 1574276914,
					'exp_month': 12,
					'exp_year': 2030,
					'failure_reason': null,
					'funding': 'credit',
					'gateway': 'test',
					'gateway_customer': null,
					'gateway_id': '5dd58f32624a4',
					'id': 7980,
					'last4': '4242',
					'merchant_account': null,
					'receipt_email': 'example@example.com',
					'object': 'card'
				},
				'pdf_url': 'https://ajwt.sandbox.invoiced.com/payments/LyGsrTkfXu2QN9w7stT2BYvX/pdf',
				'status': 'succeeded',
				'type': 'charge'
			}";

            var JsonRequest = @"{
                'notes': 'example'
                }";

            var mockHttp = new MockHttpMessageHandler();
            var httpPatch = new HttpMethod("PATCH");
            var request = mockHttp.When(httpPatch, "https://testmode/payments/656093").WithJson(JsonRequest)
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var payment = CreateDefaultPayment(client);

            payment.Notes = "example";

            payment.SaveAll();

            Assert.True(payment.Id == 656093);
            Assert.True(payment.Notes == "example");
        }

        [Fact]
        public void TestVoid()
        {
            var mockHttp = new MockHttpMessageHandler();
            var jsonResponse = @"{
	            'ach_sender_id': null,
	            'amount': 39,
	            'balance': 39,
	            'charge': null,
	            'created_at': 1586880432,
	            'currency': 'usd',
	            'customer': null,
	            'date': 1586880431,
	            'id': 61,
	            'matched': true,
	            'method': 'other',
	            'notes': null,
	            'object': 'payment',
	            'pdf_url': 'https://dundermifflin.invoiced.com/payments/59FHO96idoXFeiBDu1y5Zggg/pdf',
	            'reference': null,
	            'source': 'bank_feed',
	            'voided': true
	        }";

            var request = mockHttp.When(HttpMethod.Delete, "https://testmode/payments/656093")
		            .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var payment = CreateDefaultPayment(client);

            payment.Delete();
        }

        [Fact]
        public void TestListAll()
        {
            var jsonResponseListAll = @"[{
				'amount': 9999,
				'created_at': 1574374740,
				'credit_note': null,
				'currency': 'usd',
				'customer': 594287,
				'date': 1574374740,
				'gateway': 'test',
				'gateway_id': '5dd70d54b17eb',
				'id': 656093,
				'invoice': 2356243,
				'metadata': {},
				'method': 'credit_card',
				'notes': null,
				'object': 'payment',
				'parent_payment': null,
				'payment_source': {
					'brand': 'Visa',
					'chargeable': true,
					'created_at': 1574276914,
					'exp_month': 12,
					'exp_year': 2030,
					'failure_reason': null,
					'funding': 'credit',
					'gateway': 'test',
					'gateway_customer': null,
					'gateway_id': '5dd58f32624a4',
					'id': 7980,
					'last4': '4242',
					'merchant_account': null,
					'receipt_email': 'example@example.com',
					'object': 'card'
				},
				'pdf_url': 'https://ajwt.sandbox.invoiced.com/payments/LyGsrTkfXu2QN9w7stT2BYvX/pdf',
				'status': 'succeeded',
				'type': 'charge'
			}]";

            var mockHttp = new MockHttpMessageHandler();

            var mockHeader = new Dictionary<string, string>();
            mockHeader["X-Total-Count"] = "1";
            mockHeader["Link"] =
                "<https://api.sandbox.invoiced.com/payments?page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/payments?page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/payments?page=1>; rel=\"last\"";

            var request = mockHttp.When(HttpMethod.Get, "https://testmode/payments")
                .Respond(mockHeader, "application/json", jsonResponseListAll);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var payment = conn.NewPayment();

            var payments = payment.ListAll();

            Assert.True(payments[0].Id == 656093);
        }
    }
}