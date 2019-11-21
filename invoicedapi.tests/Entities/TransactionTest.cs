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

	public class TransactionTest
	{

		public static Transaction CreateDefaultTransaction(HttpClient client)
		{
			var json = @"{'id': 656093
                }";

			var transaction = JsonConvert.DeserializeObject<Transaction>(json);

			var connection = new Connection("voodoo", Invoiced.Environment.test);

			connection.TestClient(client);

			transaction.ChangeConnection(connection);

			return transaction;
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
				'object': 'transaction',
				'parent_transaction': null,
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

			var transaction = JsonConvert.DeserializeObject<Transaction>(json);

			Assert.True(transaction.Id == 656093);
			Assert.True(transaction.Status == "succeeded");
		}

		[Fact]
		public void TestRetrieve()
		{

			var mockHttp = new MockHttpMessageHandler();

			mockHttp.When("https://testmode/transactions/656093")
				.Respond("application/json", "{'id' : 656093, 'currency': 'usd'}");

			var client = mockHttp.ToHttpClient();

			var conn = new Connection("voodoo", Invoiced.Environment.test);

			conn.TestClient(client);

			var transactionConn = conn.NewTransaction();

			var transaction = transactionConn.Retrieve(656093);

			Assert.True(transaction.Currency == "usd");

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
				'object': 'transaction',
				'parent_transaction': null,
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

			mockHttp.When(HttpMethod.Post, "https://testmode/transactions")
				.Respond("application/json", jsonResponse);

			var client = mockHttp.ToHttpClient();

			var conn = new Connection("voodoo", Invoiced.Environment.test);

			conn.TestClient(client);

			var transaction = conn.NewTransaction();

			transaction.Create();

			Assert.True(transaction.Id == 656093);
			Assert.True(transaction.Type == "charge");

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
				'object': 'transaction',
				'parent_transaction': null,
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
			var request = mockHttp.When(httpPatch, "https://testmode/transactions/656093").WithJson(JsonRequest)
				.Respond("application/json", jsonResponse);

			var client = mockHttp.ToHttpClient();

			var transaction = CreateDefaultTransaction(client);

			transaction.Notes = "example";

			transaction.SaveAll();

			Assert.True(transaction.Id == 656093);
			Assert.True(transaction.Notes == "example");

		}

		[Fact]
		public void TestDelete()
		{

			var mockHttp = new MockHttpMessageHandler();

			var request = mockHttp.When(HttpMethod.Delete, "https://testmode/transactions/656093")
				.Respond(HttpStatusCode.NoContent);

			var client = mockHttp.ToHttpClient();

			var transaction = CreateDefaultTransaction(client);

			transaction.Delete();

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
				'object': 'transaction',
				'parent_transaction': null,
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
				"<https://api.sandbox.invoiced.com/transactions?page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/transactions?page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/transactions?page=1>; rel=\"last\"";

			var request = mockHttp.When(HttpMethod.Get, "https://testmode/transactions")
				.Respond(mockHeader, "application/json", jsonResponseListAll);

			var client = mockHttp.ToHttpClient();

			var conn = new Connection("voodoo", Invoiced.Environment.test);

			conn.TestClient(client);

			var transaction = conn.NewTransaction();

			var transactions = transaction.ListAll();

			Assert.True(transactions[0].Id == 656093);

		}
		
		[Fact]
		public void TestInitiateCharge()
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
				'invoice': 123456789,
				'metadata': {},
				'method': 'credit_card',
				'notes': null,
				'object': 'transaction',
				'parent_transaction': null,
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

			mockHttp.When(HttpMethod.Post, "https://testmode/charges")
				.Respond("application/json", jsonResponse);

			var client = mockHttp.ToHttpClient();

			var conn = new Connection("voodoo", Invoiced.Environment.test);

			conn.TestClient(client);

			var chargeRequest = new ChargeRequest {Customer = 594287, Amount = 9999, Method = "credit_card"};
			var split = new ChargeSplit {Type = "invoice", Amount = 9999, Invoice = 123456789};

			var charge = conn.NewTransaction().InitiateCharge(chargeRequest);

			Assert.True(charge.Id == 656093);
			Assert.True(charge.Invoice == 123456789);

		}
		
		[Fact]
		public void TestRefund()
		{

			var jsonResponse = @"{
				'amount': 400,
				'created_at': 1415228628,
				'credit_note': null,
				'currency': 'usd',
				'customer': 15460,
				'date': 1410843600,
				'gateway': null,
				'gateway_id': null,
				'id': 20952,
				'invoice': 44648,
				'metadata': {},
				'method': 'check',
				'notes': null,
				'object': 'transaction',
				'parent_transaction': 20939,
				'payment_source': null,
				'pdf_url': 'https://dundermifflin.invoiced.com/payments/IZmXbVOPyvfD3GPBmyd6FwXY/20939pdf',
				'status': 'succeeded',
				'type': 'refund'
			  }";

			var mockHttp = new MockHttpMessageHandler();
			var httpPatch = new HttpMethod("POST");
			var request = mockHttp.When(HttpMethod.Post, "https://testmode/transactions/656093/refunds")
				.Respond("application/json", jsonResponse);

			var client = mockHttp.ToHttpClient();

			var transaction = CreateDefaultTransaction(client);

			var refund = transaction.Refund(400);

			Assert.True(refund.Id == 20952);
			Assert.True(refund.Notes == null);

		}

	}

}
