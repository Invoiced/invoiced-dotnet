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

	public class CreditNoteTest
	{
		private static Customer CreateDefaultCustomer(HttpClient client)
		{
			var json = @"{'id': 1234
                }";

			var customer = JsonConvert.DeserializeObject<Customer>(json);

			var connection = new Connection("voodoo", Invoiced.Environment.test);

			connection.TestClient(client);

			customer.ChangeConnection(connection);

			return customer;

		}

		private static Invoice CreateDefaultInvoice(HttpClient client)
		{
			var json = @"{'id': 2334745
                }";

			var invoice = JsonConvert.DeserializeObject<Invoice>(json);

			var connection = new Connection("voodoo", Invoiced.Environment.test);

			connection.TestClient(client);

			invoice.ChangeConnection(connection);

			return invoice;

		}

		private static CreditNote CreateDefaultCreditNote(HttpClient client)
		{
			var json = @"{'id': 8441,
				'invoice': 2334745
                }";

			var creditNote = JsonConvert.DeserializeObject<CreditNote>(json);

			var connection = new Connection("voodoo", Invoiced.Environment.test);

			connection.TestClient(client);

			creditNote.ChangeConnection(connection);

			return creditNote;

		}

		[Fact]
		public void TestDeserialize()
		{
			var json = @"{
				'balance': 0,
				'closed': true,
				'created_at': 1574371071,
				'currency': 'usd',
				'customer': 594287,
				'date': 1574371069,
				'discounts': [],
				'draft': false,
				'id': 8441,
				'invoice': 2356205,
				'items': [{
					'amount': 123,
					'catalog_item': null,
					'created_at': 1574371071,
					'description': '',
					'discountable': true,
					'id': 22932564,
					'name': 'ABC',
					'quantity': 1,
					'taxable': true,
					'type': null,
					'unit_cost': 123,
					'object': 'line_item',
					'metadata': {},
					'discounts': [],
					'taxes': []
				}],
				'metadata': {
					'account_manager': 'https://www.google.com/'
				},
				'name': 'Credit Note',
				'notes': null,
				'number': 'CN-00003',
				'object': 'credit_note',
				'paid': true,
				'pdf_url': 'https://ajwt.sandbox.invoiced.com/credit_notes/Oh08vH0WUnVauNfmz4SRrXqj/pdf',
				'shipping': [],
				'status': 'paid',
				'subtotal': 123,
				'taxes': [],
				'total': 123,
				'url': 'https://ajwt.sandbox.invoiced.com/credit_notes/Oh08vH0WUnVauNfmz4SRrXqj'
			}";

			var creditNote = JsonConvert.DeserializeObject<CreditNote>(json);

			Assert.True(creditNote.Id == 8441);
			Assert.True(creditNote.Name == "Credit Note");
			Assert.True(creditNote.Subtotal == 123);
		}


		[Fact]
		public void TestRetrieve()
		{

			var mockHttp = new MockHttpMessageHandler();

			mockHttp.When("https://testmode/credit_notes/4")
				.Respond("application/json", "{'id' : 4, 'number' : 'CN-0001'}");

			var client = mockHttp.ToHttpClient();

			var conn = new Connection("voodoo", Invoiced.Environment.test);

			conn.TestClient(client);

			var creditNoteConn = conn.NewCreditNote();

			var creditNote = creditNoteConn.Retrieve(4);

			Assert.True(creditNote.Number == "CN-0001");

		}


		[Fact]
		public void TestCreate()
		{

			var jsonResponse = @"{
				'balance': 0,
				'closed': true,
				'created_at': 1574371071,
				'currency': 'usd',
				'customer': 594287,
				'date': 1574371069,
				'discounts': [],
				'draft': false,
				'id': 8441,
				'invoice': 2356205,
				'items': [{
					'amount': 123,
					'catalog_item': null,
					'created_at': 1574371071,
					'description': '',
					'discountable': true,
					'id': 22932564,
					'name': 'ABC',
					'quantity': 1,
					'taxable': true,
					'type': null,
					'unit_cost': 123,
					'object': 'line_item',
					'metadata': {},
					'discounts': [],
					'taxes': []
				}],
				'metadata': {
					'account_manager': 'https://www.google.com/'
				},
				'name': 'Credit Note',
				'notes': null,
				'number': 'CN-00003',
				'object': 'credit_note',
				'paid': true,
				'pdf_url': 'https://ajwt.sandbox.invoiced.com/credit_notes/Oh08vH0WUnVauNfmz4SRrXqj/pdf',
				'shipping': [],
				'status': 'paid',
				'subtotal': 123,
				'taxes': [],
				'total': 123,
				'url': 'https://ajwt.sandbox.invoiced.com/credit_notes/Oh08vH0WUnVauNfmz4SRrXqj'
			}";

			var mockHttp = new MockHttpMessageHandler();

			mockHttp.When(HttpMethod.Post, "https://testmode/credit_notes").Respond("application/json", jsonResponse);

			var client = mockHttp.ToHttpClient();

			var conn = new Connection("voodoo", Invoiced.Environment.test);

			conn.TestClient(client);

			var creditNote = conn.NewCreditNote();

			creditNote.Create();

			Assert.True(creditNote.Id == 8441);
			Assert.True(creditNote.Number == "CN-00003");

		}

		[Fact]
		public void TestSave()
		{

			var jsonResponse = @"{
				'balance': 0,
				'closed': true,
				'created_at': 1574371071,
				'currency': 'usd',
				'customer': 594287,
				'date': 1574371069,
				'discounts': [],
				'draft': false,
				'id': 8441,
				'invoice': 2356205,
				'items': [{
					'amount': 123,
					'catalog_item': null,
					'created_at': 1574371071,
					'description': '',
					'discountable': true,
					'id': 22932564,
					'name': 'ABC',
					'quantity': 1,
					'taxable': true,
					'type': null,
					'unit_cost': 123,
					'object': 'line_item',
					'metadata': {},
					'discounts': [],
					'taxes': []
				}],
				'metadata': {
					'account_manager': 'https://www.google.com/'
				},
				'name': 'Updated',
				'notes': null,
				'number': 'CN-00003',
				'object': 'credit_note',
				'paid': true,
				'pdf_url': 'https://ajwt.sandbox.invoiced.com/credit_notes/Oh08vH0WUnVauNfmz4SRrXqj/pdf',
				'shipping': [],
				'status': 'paid',
				'subtotal': 123,
				'taxes': [],
				'total': 123,
				'url': 'https://ajwt.sandbox.invoiced.com/credit_notes/Oh08vH0WUnVauNfmz4SRrXqj'
			}";

			var mockHttp = new MockHttpMessageHandler();
			var httpPatch = new HttpMethod("PATCH");
			var request = mockHttp.When(httpPatch, "https://testmode/credit_notes/8441")
				.Respond("application/json", jsonResponse);

			var client = mockHttp.ToHttpClient();

			var creditNote = CreateDefaultCreditNote(client);

			creditNote.Name = "Updated";

			creditNote.SaveAll();

			Assert.True(creditNote.Name == "Updated");

		}

		[Fact]
		public void TestDelete()
		{

			var mockHttp = new MockHttpMessageHandler();

			var request = mockHttp.When(HttpMethod.Delete, "https://testmode/credit_notes/8441")
				.Respond(HttpStatusCode.NoContent);

			var client = mockHttp.ToHttpClient();

			var creditNote = CreateDefaultCreditNote(client);

			creditNote.Delete();

		}


		[Fact]
		public void TestListAll()
		{

			var jsonResponseListAll = @"[{
				'balance': 0,
				'closed': true,
				'created_at': 1574371071,
				'currency': 'usd',
				'customer': 594287,
				'date': 1574371069,
				'discounts': [],
				'draft': false,
				'id': 8441,
				'invoice': 2356205,
				'items': [{
					'amount': 123,
					'catalog_item': null,
					'created_at': 1574371071,
					'description': '',
					'discountable': true,
					'id': 22932564,
					'name': 'ABC',
					'quantity': 1,
					'taxable': true,
					'type': null,
					'unit_cost': 123,
					'object': 'line_item',
					'metadata': {},
					'discounts': [],
					'taxes': []
				}],
				'metadata': {
					'account_manager': 'https://www.google.com/'
				},
				'name': 'Credit Note',
				'notes': null,
				'number': 'CN-00003',
				'object': 'credit_note',
				'paid': true,
				'pdf_url': 'https://ajwt.sandbox.invoiced.com/credit_notes/Oh08vH0WUnVauNfmz4SRrXqj/pdf',
				'shipping': [],
				'status': 'paid',
				'subtotal': 123,
				'taxes': [],
				'total': 123,
				'url': 'https://ajwt.sandbox.invoiced.com/credit_notes/Oh08vH0WUnVauNfmz4SRrXqj'
			}]";

			var mockHttp = new MockHttpMessageHandler();

			var mockHeader = new Dictionary<string, string>();
			mockHeader["X-Total-Count"] = "1";
			mockHeader["Link"] =
				"<https://api.sandbox.invoiced.com/credit_notes?page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/credit_notes?page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/credit_notes?page=1>; rel=\"last\"";

			var request = mockHttp.When(HttpMethod.Get, "https://testmode/credit_notes")
				.Respond(mockHeader, "application/json", jsonResponseListAll);

			var client = mockHttp.ToHttpClient();

			var conn = new Connection("voodoo", Invoiced.Environment.test);

			conn.TestClient(client);

			var creditNote = conn.NewCreditNote();

			var creditNotes = creditNote.ListAll();

			Assert.True(creditNotes[0].Id == 8441);
			Assert.True(creditNotes[0].Number == "CN-00003");

		}

	}

}
