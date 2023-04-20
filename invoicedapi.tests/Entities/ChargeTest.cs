using System.Net.Http;
using Invoiced;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace InvoicedTest
{
    public class ChargeTest
    {
        public static Payment CreateDefaultTransaction(HttpClient client)
        {
            var json = @"{'id': 656093
                }";

            var transaction = JsonConvert.DeserializeObject<Payment>(json);

            var connection = new Connection("voodoo", Environment.test);

            connection.TestClient(client);

            transaction.ChangeConnection(connection);

            return transaction;
        }

        [Fact]
        public void TestCrate()
        {
            var jsonResponse = @"{
				'ach_sender_id': null,
				'amount': 2000,
				'balance': 0,
				'charge': {
					'amount': 2000,
					'amount_refunded': 0,
					'created_at': 1607152637,
					'currency': 'usd',
					'customer': 401558,
					'disputed': false,
					'failure_message': null,
					'gateway': 'test',
					'gateway_id': '5d9e31d812151',
					'id': 46374,
					'payment_source': {
						'brand': 'Visa',
						'chargeable': true,
						'created_at': 1601919571,
						'exp_month': 8,
						'exp_year': 2025,
						'failure_reason': null,
						'funding': 'credit',
						'gateway': null,
						'gateway_customer': null,
						'gateway_id': null,
						'id': 448165,
						'last4': '8550',
						'merchant_account': 2353,
						'object': 'card',
						'receipt_email': null
					},
					'receipt_email': null,
					'refunded': false,
					'refunds': [],
					'status': 'succeeded'
				},
				'created_at': 1607152637,
				'currency': 'usd',
				'customer': 401558,
				'date': 1607152637,
				'id': 32554,
				'matched': null,
				'method': 'credit_card',
				'notes': null,
				'object': 'payment',
				'pdf_url': 'https://dundermifflin.invoiced.com/payments/dPf17GiBjJYqcl0zjm7jc02a/pdf',
				'reference': '5d9e31d812151',
				'source': 'api',
				'voided': false
			}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://testmode/charges")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var chargeRequest = new ChargeRequest {Customer = 594287, Amount = 9999, Method = "credit_card"};
            chargeRequest.AppliedTo = new[]
            {
	            new PaymentItem {Type = "invoice", Amount = 9999, Invoice = 123456789}
            };

            var payment = conn.NewCharge().Create(chargeRequest);

            Assert.True(payment.Id == 32554);
            Assert.True(payment.Charge.Id == 46374);
        }
        [Fact]
        public async Task TestCrateAsync()
        {
            var jsonResponse = @"{
				'ach_sender_id': null,
				'amount': 2000,
				'balance': 0,
				'charge': {
					'amount': 2000,
					'amount_refunded': 0,
					'created_at': 1607152637,
					'currency': 'usd',
					'customer': 401558,
					'disputed': false,
					'failure_message': null,
					'gateway': 'test',
					'gateway_id': '5d9e31d812151',
					'id': 46374,
					'payment_source': {
						'brand': 'Visa',
						'chargeable': true,
						'created_at': 1601919571,
						'exp_month': 8,
						'exp_year': 2025,
						'failure_reason': null,
						'funding': 'credit',
						'gateway': null,
						'gateway_customer': null,
						'gateway_id': null,
						'id': 448165,
						'last4': '8550',
						'merchant_account': 2353,
						'object': 'card',
						'receipt_email': null
					},
					'receipt_email': null,
					'refunded': false,
					'refunds': [],
					'status': 'succeeded'
				},
				'created_at': 1607152637,
				'currency': 'usd',
				'customer': 401558,
				'date': 1607152637,
				'id': 32554,
				'matched': null,
				'method': 'credit_card',
				'notes': null,
				'object': 'payment',
				'pdf_url': 'https://dundermifflin.invoiced.com/payments/dPf17GiBjJYqcl0zjm7jc02a/pdf',
				'reference': '5d9e31d812151',
				'source': 'api',
				'voided': false
			}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://testmode/charges")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var chargeRequest = new ChargeRequest {Customer = 594287, Amount = 9999, Method = "credit_card"};
            chargeRequest.AppliedTo = new[]
            {
	            new PaymentItem {Type = "invoice", Amount = 9999, Invoice = 123456789}
            };

            var payment = await conn.NewCharge().CreateAsync(chargeRequest);

            Assert.True(payment.Id == 32554);
            Assert.True(payment.Charge.Id == 46374);
        }
    }
}