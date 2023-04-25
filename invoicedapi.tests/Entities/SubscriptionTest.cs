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
    public class SubscriptionTest
    {
        public static Subscription CreateDefaultSubscription(HttpClient client)
        {
            var json = @"{'id': 13117
                }";

            var subscription = JsonConvert.DeserializeObject<Subscription>(json);

            var connection = new Connection("voodoo", Environment.test);

            connection.TestClient(client);

            subscription.ChangeConnection(connection);

            return subscription;
        }

        [Fact]
        public void TestDeserialize()
        {
            var json = @"{
				'addons': [{
					'catalog_item': null,
					'created_at': 1574374281,
					'description': '',
					'id': 3495,
					'plan': 'alpha',
					'quantity': 1,
					'object': 'subscription_addon'
				}],
				'amount': 10.99,
				'approval': null,
				'bill_in': 'advance',
				'bill_in_advance_days': 0,
				'cancel_at_period_end': false,
				'canceled_at': null,
				'contract_period_end': 1576908000,
				'contract_period_start': 1574316000,
				'contract_renewal_cycles': null,
				'contract_renewal_mode': 'auto',
				'created_at': 1574373324,
				'customer': 594287,
				'cycles': 1,
				'description': null,
				'discounts': [],
				'id': 13117,
				'metadata': {},
				'mrr': 49,
				'object': 'subscription',
				'paused': false,
				'payment_source': null,
				'period_end': 1576907999,
				'period_offset_days': 0,
				'period_start': 1574316000,
				'plan': 'starter',
				'quantity': 1,
				'recurring_total': 49,
				'renewed_last': 1574316000,
				'renews_next': 1576908000,
				'ship_to': null,
				'start_date': 1574316000,
				'status': 'active',
				'taxes': [],
				'url': 'https://ajwt.sandbox.invoiced.com/subscriptions/odtQWtUJ8VVaFbicpJ57AER9'
			}";

            var testEvent = JsonConvert.DeserializeObject<Subscription>(json);

            Assert.True(testEvent.Id == 13117);
            Assert.True(testEvent.Status == "active");
            Assert.True(testEvent.Amount == 10.99);
        }

        [Fact]
        public void TestRetrieve()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("https://testmode/subscriptions/13117")
                .Respond("application/json", "{'id' : 13117, 'cycles': 1}");

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var subscriptionConn = conn.NewSubscription();

            var subscription = subscriptionConn.Retrieve(13117);

            Assert.True(subscription.Cycles == 1);
        }


        [Fact]
        public void TestCreate()
        {
            var jsonResponse = @"{
				'addons': [],
				'approval': null,
				'bill_in': 'advance',
				'bill_in_advance_days': 0,
				'cancel_at_period_end': false,
				'canceled_at': null,
				'contract_period_end': 1576908000,
				'contract_period_start': 1574316000,
				'contract_renewal_cycles': null,
				'contract_renewal_mode': 'auto',
				'created_at': 1574373324,
				'customer': 594287,
				'cycles': 1,
				'description': null,
				'discounts': [],
				'id': 13117,
				'metadata': {},
				'mrr': 49,
				'object': 'subscription',
				'paused': false,
				'payment_source': null,
				'period_end': 1576907999,
				'period_offset_days': 0,
				'period_start': 1574316000,
				'plan': 'starter',
				'quantity': 1,
				'recurring_total': 49,
				'renewed_last': 1574316000,
				'renews_next': 1576908000,
				'ship_to': null,
				'start_date': 1574316000,
				'status': 'active',
				'taxes': [],
				'url': 'https://ajwt.sandbox.invoiced.com/subscriptions/odtQWtUJ8VVaFbicpJ57AER9'
			}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://testmode/subscriptions")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var subscription = conn.NewSubscription();

            subscription.Create();

            Assert.True(subscription.Id == 13117);
            Assert.True(subscription.StartDate == 1574316000);
        }

        [Fact]
        public void TestSave()
        {
            var jsonResponse = @"{
				'addons': [],
				'approval': null,
				'bill_in': 'advance',
				'bill_in_advance_days': 0,
				'cancel_at_period_end': true,
				'canceled_at': null,
				'contract_period_end': 1576908000,
				'contract_period_start': 1574316000,
				'contract_renewal_cycles': null,
				'contract_renewal_mode': 'auto',
				'created_at': 1574373324,
				'customer': 594287,
				'cycles': 1,
				'description': null,
				'discounts': [],
				'id': 13117,
				'metadata': {},
				'mrr': 49,
				'object': 'subscription',
				'paused': false,
				'payment_source': null,
				'period_end': 1576907999,
				'period_offset_days': 0,
				'period_start': 1574316000,
				'plan': 'starter',
				'quantity': 1,
				'recurring_total': 49,
				'renewed_last': 1574316000,
				'renews_next': 1576908000,
				'ship_to': null,
				'start_date': 1574316000,
				'status': 'active',
				'taxes': [],
				'url': 'https://ajwt.sandbox.invoiced.com/subscriptions/odtQWtUJ8VVaFbicpJ57AER9'
			}";


            var JsonRequest = @"{
                'cancel_at_period_end': true
                }";

            var mockHttp = new MockHttpMessageHandler();
            var httpPatch = new HttpMethod("PATCH");
            var request = mockHttp.When(httpPatch, "https://testmode/subscriptions/13117").WithJson(JsonRequest)
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var subscription = CreateDefaultSubscription(client);

            subscription.CancelAtPeriodEnd = true;

            subscription.SaveAll();

            Assert.True(subscription.Id == 13117);
            Assert.True(subscription.CancelAtPeriodEnd == true);
        }

        [Fact]
        public void TestDelete()
        {
            var mockHttp = new MockHttpMessageHandler();

            var request = mockHttp.When(HttpMethod.Delete, "https://testmode/subscriptions/13117")
                .Respond(HttpStatusCode.NoContent);

            var client = mockHttp.ToHttpClient();

            var subscription = CreateDefaultSubscription(client);

            subscription.Delete();
        }

        [Fact]
        public void TestCancel()
        {
            var mockHttp = new MockHttpMessageHandler();

            var request = mockHttp.When(HttpMethod.Delete, "https://testmode/subscriptions/13117")
                .Respond(HttpStatusCode.NoContent);

            var client = mockHttp.ToHttpClient();

            var subscription = CreateDefaultSubscription(client);

            subscription.Cancel();
        }


        [Fact]
        public void TestListAll()
        {
            var jsonResponseListAll = @"[{
                'addons': [],
                'approval': null,
                'bill_in': 'advance',
                'bill_in_advance_days': 0,
                'cancel_at_period_end': true,
                'canceled_at': null,
                'contract_period_end': 1576908000,
                'contract_period_start': 1574316000,
                'contract_renewal_cycles': null,
                'contract_renewal_mode': 'auto',
                'created_at': 1574373324,
                'customer': 594287,
                'cycles': 1,
                'description': null,
                'discounts': [],
                'id': 13117,
                'metadata': {},
                'mrr': 49,
                'object': 'subscription',
                'paused': false,
                'payment_source': null,
                'period_end': 1576907999,
                'period_offset_days': 0,
                'period_start': 1574316000,
                'plan': 'starter',
                'quantity': 1,
                'recurring_total': 49,
                'renewed_last': 1574316000,
                'renews_next': 1576908000,
                'ship_to': null,
                'start_date': 1574316000,
                'status': 'active',
                'taxes': [],
                'url': 'https://ajwt.sandbox.invoiced.com/subscriptions/odtQWtUJ8VVaFbicpJ57AER9'
            }]";

            var mockHttp = new MockHttpMessageHandler();

            var mockHeader = new Dictionary<string, string>();
            mockHeader["X-Total-Count"] = "1";
            mockHeader["Link"] =
                "<https://api.sandbox.invoiced.com/subscriptions?page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/subscriptions?page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/subscriptions?page=1>; rel=\"last\"";

            var request = mockHttp.When(HttpMethod.Get, "https://testmode/subscriptions")
                .Respond(mockHeader, "application/json", jsonResponseListAll);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var subscription = conn.NewSubscription();

            var subscriptions = subscription.ListAll();

            Assert.True(subscriptions[0].Id == 13117);
        }

        [Fact]
        public void TestPreview()
        {
            var jsonResponse = @"{
				'first_invoice': {
					'attempt_count': null,
					'autopay': null,
					'balance': 0,
					'chase': false,
					'closed': false,
					'created_at': null,
					'csv_url': null,
					'currency': 'usd',
					'customer': -1,
					'date': 1571410119,
					'discounts': [],
					'draft': true,
					'due_date': null,
					'id': false,
					'items': [{
						'amount': 49,
						'catalog_item': null,
						'created_at': null,
						'description': '',
						'discountable': true,
						'discounts': [],
						'id': false,
						'metadata': {},
						'name': 'Starter',
						'object': 'line_item',
						'plan': 'starter',
						'quantity': 1,
						'taxable': true,
						'taxes': [],
						'type': 'plan',
						'unit_cost': 49
					}],
					'metadata': {},
					'name': 'Starter',
					'needs_attention': null,
					'next_chase_on': null,
					'next_payment_attempt': null,
					'notes': null,
					'number': null,
					'object': 'invoice',
					'paid': false,
					'payment_plan': null,
					'payment_source': null,
					'payment_terms': null,
					'payment_url': null,
					'pdf_url': null,
					'purchase_order': null,
					'ship_to': null,
					'shipping': [],
					'status': 'draft',
					'subscription': false,
					'subtotal': 49,
					'taxes': [],
					'total': 49,
					'url': null
				},
				'mrr': 49,
				'recurring_total': 49
			}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://testmode/subscriptions/preview")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var subscription = conn.NewSubscription();

            var preview = subscription.Preview();

            Assert.True(preview.Mrr == 49);
            Assert.True(preview.RecurringTotal == 49);
        }
        [Fact]
        public async Task TestRetrieveAsync()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("https://testmode/subscriptions/13117")
                .Respond("application/json", "{'id' : 13117, 'cycles': 1}");

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var subscriptionConn = conn.NewSubscription();

            var subscription = await subscriptionConn.RetrieveAsync(13117);

            Assert.True(subscription.Cycles == 1);
        }


        [Fact]
        public async Task TestCreateAsync()
        {
            var jsonResponse = @"{
				'addons': [],
				'approval': null,
				'bill_in': 'advance',
				'bill_in_advance_days': 0,
				'cancel_at_period_end': false,
				'canceled_at': null,
				'contract_period_end': 1576908000,
				'contract_period_start': 1574316000,
				'contract_renewal_cycles': null,
				'contract_renewal_mode': 'auto',
				'created_at': 1574373324,
				'customer': 594287,
				'cycles': 1,
				'description': null,
				'discounts': [],
				'id': 13117,
				'metadata': {},
				'mrr': 49,
				'object': 'subscription',
				'paused': false,
				'payment_source': null,
				'period_end': 1576907999,
				'period_offset_days': 0,
				'period_start': 1574316000,
				'plan': 'starter',
				'quantity': 1,
				'recurring_total': 49,
				'renewed_last': 1574316000,
				'renews_next': 1576908000,
				'ship_to': null,
				'start_date': 1574316000,
				'status': 'active',
				'taxes': [],
				'url': 'https://ajwt.sandbox.invoiced.com/subscriptions/odtQWtUJ8VVaFbicpJ57AER9'
			}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://testmode/subscriptions")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var subscription = conn.NewSubscription();

            await subscription.CreateAsync();

            Assert.True(subscription.Id == 13117);
            Assert.True(subscription.StartDate == 1574316000);
        }

        [Fact]
        public async Task TestSaveAsync()
        {
            var jsonResponse = @"{
				'addons': [],
				'approval': null,
				'bill_in': 'advance',
				'bill_in_advance_days': 0,
				'cancel_at_period_end': true,
				'canceled_at': null,
				'contract_period_end': 1576908000,
				'contract_period_start': 1574316000,
				'contract_renewal_cycles': null,
				'contract_renewal_mode': 'auto',
				'created_at': 1574373324,
				'customer': 594287,
				'cycles': 1,
				'description': null,
				'discounts': [],
				'id': 13117,
				'metadata': {},
				'mrr': 49,
				'object': 'subscription',
				'paused': false,
				'payment_source': null,
				'period_end': 1576907999,
				'period_offset_days': 0,
				'period_start': 1574316000,
				'plan': 'starter',
				'quantity': 1,
				'recurring_total': 49,
				'renewed_last': 1574316000,
				'renews_next': 1576908000,
				'ship_to': null,
				'start_date': 1574316000,
				'status': 'active',
				'taxes': [],
				'url': 'https://ajwt.sandbox.invoiced.com/subscriptions/odtQWtUJ8VVaFbicpJ57AER9'
			}";


            var JsonRequest = @"{
                'cancel_at_period_end': true
                }";

            var mockHttp = new MockHttpMessageHandler();
            var httpPatch = new HttpMethod("PATCH");
            var request = mockHttp.When(httpPatch, "https://testmode/subscriptions/13117").WithJson(JsonRequest)
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var subscription = CreateDefaultSubscription(client);

            subscription.CancelAtPeriodEnd = true;

            await subscription.SaveAllAsync();

            Assert.True(subscription.Id == 13117);
            Assert.True(subscription.CancelAtPeriodEnd == true);
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            var mockHttp = new MockHttpMessageHandler();

            var request = mockHttp.When(HttpMethod.Delete, "https://testmode/subscriptions/13117")
                .Respond(HttpStatusCode.NoContent);

            var client = mockHttp.ToHttpClient();

            var subscription = CreateDefaultSubscription(client);

            await subscription.DeleteAsync();
        }

        [Fact]
        public async Task TestCancelAsync()
        {
            var mockHttp = new MockHttpMessageHandler();

            var request = mockHttp.When(HttpMethod.Delete, "https://testmode/subscriptions/13117")
                .Respond(HttpStatusCode.NoContent);

            var client = mockHttp.ToHttpClient();

            var subscription = CreateDefaultSubscription(client);

            await subscription.CancelAsync();
        }


        [Fact]
        public async Task TestListAllAsync()
        {
            var jsonResponseListAll = @"[{
                'addons': [],
                'approval': null,
                'bill_in': 'advance',
                'bill_in_advance_days': 0,
                'cancel_at_period_end': true,
                'canceled_at': null,
                'contract_period_end': 1576908000,
                'contract_period_start': 1574316000,
                'contract_renewal_cycles': null,
                'contract_renewal_mode': 'auto',
                'created_at': 1574373324,
                'customer': 594287,
                'cycles': 1,
                'description': null,
                'discounts': [],
                'id': 13117,
                'metadata': {},
                'mrr': 49,
                'object': 'subscription',
                'paused': false,
                'payment_source': null,
                'period_end': 1576907999,
                'period_offset_days': 0,
                'period_start': 1574316000,
                'plan': 'starter',
                'quantity': 1,
                'recurring_total': 49,
                'renewed_last': 1574316000,
                'renews_next': 1576908000,
                'ship_to': null,
                'start_date': 1574316000,
                'status': 'active',
                'taxes': [],
                'url': 'https://ajwt.sandbox.invoiced.com/subscriptions/odtQWtUJ8VVaFbicpJ57AER9'
            }]";

            var mockHttp = new MockHttpMessageHandler();

            var mockHeader = new Dictionary<string, string>();
            mockHeader["X-Total-Count"] = "1";
            mockHeader["Link"] =
                "<https://api.sandbox.invoiced.com/subscriptions?page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/subscriptions?page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/subscriptions?page=1>; rel=\"last\"";

            var request = mockHttp.When(HttpMethod.Get, "https://testmode/subscriptions")
                .Respond(mockHeader, "application/json", jsonResponseListAll);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var subscription = conn.NewSubscription();

            var subscriptions = await subscription.ListAllAsync();

            Assert.True(subscriptions[0].Id == 13117);
        }

        [Fact]
        public async Task TestPreviewAsync()
        {
            var jsonResponse = @"{
				'first_invoice': {
					'attempt_count': null,
					'autopay': null,
					'balance': 0,
					'chase': false,
					'closed': false,
					'created_at': null,
					'csv_url': null,
					'currency': 'usd',
					'customer': -1,
					'date': 1571410119,
					'discounts': [],
					'draft': true,
					'due_date': null,
					'id': false,
					'items': [{
						'amount': 49,
						'catalog_item': null,
						'created_at': null,
						'description': '',
						'discountable': true,
						'discounts': [],
						'id': false,
						'metadata': {},
						'name': 'Starter',
						'object': 'line_item',
						'plan': 'starter',
						'quantity': 1,
						'taxable': true,
						'taxes': [],
						'type': 'plan',
						'unit_cost': 49
					}],
					'metadata': {},
					'name': 'Starter',
					'needs_attention': null,
					'next_chase_on': null,
					'next_payment_attempt': null,
					'notes': null,
					'number': null,
					'object': 'invoice',
					'paid': false,
					'payment_plan': null,
					'payment_source': null,
					'payment_terms': null,
					'payment_url': null,
					'pdf_url': null,
					'purchase_order': null,
					'ship_to': null,
					'shipping': [],
					'status': 'draft',
					'subscription': false,
					'subtotal': 49,
					'taxes': [],
					'total': 49,
					'url': null
				},
				'mrr': 49,
				'recurring_total': 49
			}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://testmode/subscriptions/preview")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var subscription = conn.NewSubscription();

            var preview = await subscription.PreviewAsync();

            Assert.True(preview.Mrr == 49);
            Assert.True(preview.RecurringTotal == 49);
        }
    }
}