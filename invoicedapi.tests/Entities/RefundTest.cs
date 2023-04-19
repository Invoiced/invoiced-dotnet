using System.Net.Http;
using System.Threading.Tasks;
using Invoiced;
using RichardSzalay.MockHttp;
using Xunit;

namespace InvoicedTest
{
    public class RefundTest
    {
        [Fact]
        public void TestCreate()
        {
            var jsonRequest = @"{'amount': 400}";

            var jsonResponse = @"{
				'amount': 400,
				'charge': 46374,
				'created_at': 1606943682,
				'currency': 'usd',
				'failure_message': null,
				'gateway': 'test',
				'gateway_id': 'wx0c2B1he',
				'id': 5309,
				'object': 'refund',
				'status': 'succeeded'
			}";

            var mockHttp = new MockHttpMessageHandler();
            var request = mockHttp.When(HttpMethod.Post, "https://testmode/charges/46374/refunds")
                .WithJson(jsonRequest)
                .Respond("application/json", jsonResponse);

            var connection = new Connection("voodoo", Environment.test);
            connection.TestClient(mockHttp.ToHttpClient());

            var refund = connection.NewRefund().Create(46374, 400);

            Assert.True(refund.Id == 5309);
            Assert.True(refund.Charge == 46374);
        }
        [Fact]
        public async Task TestCreateAsync()
        {
            var jsonRequest = @"{'amount': 400}";

            var jsonResponse = @"{
				'amount': 400,
				'charge': 46374,
				'created_at': 1606943682,
				'currency': 'usd',
				'failure_message': null,
				'gateway': 'test',
				'gateway_id': 'wx0c2B1he',
				'id': 5309,
				'object': 'refund',
				'status': 'succeeded'
			}";

            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(HttpMethod.Post, "https://testmode/charges/46374/refunds")
                .WithJson(jsonRequest)
                .Respond("application/json", jsonResponse);

            var connection = new Connection("voodoo", Environment.test);
            connection.TestClient(mockHttp.ToHttpClient());

            var refund = await connection.NewRefund().CreateAsync(46374, 400);

            Assert.True(refund.Id == 5309);
            Assert.True(refund.Charge == 46374);
        }
    }
}