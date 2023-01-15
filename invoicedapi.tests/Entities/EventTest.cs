using System.Collections.Generic;
using System.Net.Http;
using Invoiced;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Xunit;

namespace InvoicedTest
{
    public class EventTest
    {
        [Fact]
        public void TestDeserialize()
        {
            var json = @"{
			    'data': {
			      'object': {
			        'attempt_count': 0,
			        'autopay': false,
			        'balance': 0,
			        'chase': false,
			        'closed': true,
			        'created_at': 1574371986,
			        'currency': 'usd',
			        'customer': {
			          'address1': '123 Main St',
			          'address2': null,
			          'attention_to': null,
			          'autopay': false,
			          'autopay_delay_days': -1,
			          'avalara_entity_use_code': null,
			          'avalara_exemption_number': null,
			          'bill_to_parent': false,
			          'chase': true,
			          'chasing_cadence': null,
			          'city': 'Austin',
			          'consolidated': false,
			          'country': 'US',
			          'created_at': 1574194871,
			          'credit_hold': false,
			          'credit_limit': null,
			          'email': 'example@example.com',
			          'id': 594287,
			          'language': null,
			          'name': 'Acme',
			          'next_chase_step': null,
			          'notes': null,
			          'number': 'CUST-00006',
			          'owner': 1976,
			          'parent_customer': null,
			          'payment_terms': 'NET 7',
			          'phone': '5125551212',
			          'postal_code': '78730',
			          'state': 'TX',
			          'tax_id': null,
			          'taxable': true,
			          'taxes': [],
			          'type': 'company',
			          'object': 'customer',
			          'statement_pdf_url': 'https://ajwt.sandbox.invoiced.com/statements/iqNxncYsm91S3gtpz0jtzEXY/pdf',
			          'sign_up_url': null,
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
			            'receipt_email': null,
			            'object': 'card'
			          },
			          'sign_up_page': null,
			          'metadata': {
			            'account_manager': 'https://www.google.com/'
			          }
			        },
			        'date': 1574371986,
			        'draft': false,
			        'due_date': 1574976786,
			        'id': 2356229,
			        'name': 'Invoice',
			        'needs_attention': false,
			        'next_chase_on': null,
			        'next_payment_attempt': null,
			        'notes': null,
			        'number': 'INV-000013',
			        'paid': true,
			        'payment_plan': null,
			        'payment_terms': 'NET 7',
			        'purchase_order': null,
			        'status': 'paid',
			        'subscription': null,
			        'subtotal': 0,
			        'total': 0,
			        'object': 'invoice',
			        'url': 'https://ajwt.sandbox.invoiced.com/invoices/BZ7mtbqMCKuAq0rgKfzWGZeg',
			        'pdf_url': 'https://ajwt.sandbox.invoiced.com/invoices/BZ7mtbqMCKuAq0rgKfzWGZeg/pdf',
			        'csv_url': 'https://ajwt.sandbox.invoiced.com/invoices/BZ7mtbqMCKuAq0rgKfzWGZeg/csv',
			        'payment_url': null,
			        'ship_to': null,
			        'payment_source': null,
			        'metadata': [],
			        'items': [],
			        'discounts': [],
			        'taxes': [],
			        'shipping': []
			      }
			    },
			    'id': 5309873,
			    'timestamp': 1574371986,
			    'type': 'invoice.created',
			    'user': {
			      'created_at': 1563810757,
			      'email': 'adam.train@invoiced.com',
			      'first_name': 'Adam',
			      'id': 1976,
			      'last_name': 'Train',
			      'two_factor_enabled': false,
			      'registered': true
			    }
			  }";

            var testEvent = JsonConvert.DeserializeObject<Event>(json);

            Assert.True(testEvent.Id == 5309873);
            Assert.True(testEvent.Type == "invoice.created");
        }

        [Fact]
        public void TestListAll()
        {
            var jsonResponseListAll = @"[{
			    'data': {
			      'object': {
			        'attempt_count': 0,
			        'autopay': false,
			        'balance': 0,
			        'chase': false,
			        'closed': true,
			        'created_at': 1574371986,
			        'currency': 'usd',
			        'customer': {
			          'address1': '123 Main St',
			          'address2': null,
			          'attention_to': null,
			          'autopay': false,
			          'autopay_delay_days': -1,
			          'avalara_entity_use_code': null,
			          'avalara_exemption_number': null,
			          'bill_to_parent': false,
			          'chase': true,
			          'chasing_cadence': null,
			          'city': 'Austin',
			          'consolidated': false,
			          'country': 'US',
			          'created_at': 1574194871,
			          'credit_hold': false,
			          'credit_limit': null,
			          'email': 'example@example.com',
			          'id': 594287,
			          'language': null,
			          'name': 'Acme',
			          'next_chase_step': null,
			          'notes': null,
			          'number': 'CUST-00006',
			          'owner': 1976,
			          'parent_customer': null,
			          'payment_terms': 'NET 7',
			          'phone': '5125551212',
			          'postal_code': '78730',
			          'state': 'TX',
			          'tax_id': null,
			          'taxable': true,
			          'taxes': [],
			          'type': 'company',
			          'object': 'customer',
			          'statement_pdf_url': 'https://ajwt.sandbox.invoiced.com/statements/iqNxncYsm91S3gtpz0jtzEXY/pdf',
			          'sign_up_url': null,
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
			            'receipt_email': null,
			            'object': 'card'
			          },
			          'sign_up_page': null,
			          'metadata': {
			            'account_manager': 'https://www.google.com/'
			          }
			        },
			        'date': 1574371986,
			        'draft': false,
			        'due_date': 1574976786,
			        'id': 2356229,
			        'name': 'Invoice',
			        'needs_attention': false,
			        'next_chase_on': null,
			        'next_payment_attempt': null,
			        'notes': null,
			        'number': 'INV-000013',
			        'paid': true,
			        'payment_plan': null,
			        'payment_terms': 'NET 7',
			        'purchase_order': null,
			        'status': 'paid',
			        'subscription': null,
			        'subtotal': 0,
			        'total': 0,
			        'object': 'invoice',
			        'url': 'https://ajwt.sandbox.invoiced.com/invoices/BZ7mtbqMCKuAq0rgKfzWGZeg',
			        'pdf_url': 'https://ajwt.sandbox.invoiced.com/invoices/BZ7mtbqMCKuAq0rgKfzWGZeg/pdf',
			        'csv_url': 'https://ajwt.sandbox.invoiced.com/invoices/BZ7mtbqMCKuAq0rgKfzWGZeg/csv',
			        'payment_url': null,
			        'ship_to': null,
			        'payment_source': null,
			        'metadata': [],
			        'items': [],
			        'discounts': [],
			        'taxes': [],
			        'shipping': []
			      }
			    },
			    'id': 5309873,
			    'timestamp': 1574371986,
			    'type': 'invoice.created',
			    'user': {
			      'created_at': 1563810757,
			      'email': 'adam.train@invoiced.com',
			      'first_name': 'Adam',
			      'id': 1976,
			      'last_name': 'Train',
			      'two_factor_enabled': false,
			      'registered': true
			    }
			  }]";

            var mockHttp = new MockHttpMessageHandler();

            var mockHeader = new Dictionary<string, string>();
            mockHeader["X-Total-Count"] = "1";
            mockHeader["Link"] =
                "<https://api.sandbox.invoiced.com/events?page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/events?page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/events?page=1>; rel=\"last\"";

            var request = mockHttp.When(HttpMethod.Get, "https://api.testmode.com/events")
                .Respond(mockHeader, "application/json", jsonResponseListAll);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var ev = conn.NewEvent();

            var events = ev.ListAll();

            Assert.True(events[0].Id == 5309873);
        }
    }
}