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
    public class CouponTest
    {
        private static Coupon CreateDefaultCoupon(HttpClient client)
        {
            var json = @"{'id': 'alpha'}";

            var coupon = JsonConvert.DeserializeObject<Coupon>(json);

            var connection = new Connection("voodoo", Environment.test);

            connection.TestClient(client);

            coupon.ChangeConnection(connection);

            return coupon;
        }

        [Fact]
        public void TestDeserialize()
        {
            var json = @"{
				'created_at': 1574370277,
				'currency': null,
				'duration': 0,
				'exclusive': null,
				'expiration_date': null,
				'id': 'alpha',
				'is_percent': true,
				'max_redemptions': null,
				'metadata': {},
				'name': 'Alpha',
				'object': 'coupon',
				'value': 15
			}";

            var coupon = JsonConvert.DeserializeObject<Coupon>(json);

            Assert.True(coupon.Name == "Alpha");
            Assert.True(coupon.CreatedAt == 1574370277);
        }


        [Fact]
        public void TestRetrieve()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("https://testmode/coupons/alpha")
                .Respond("application/json", "{'id' : 'alpha', 'name' : 'Alpha'}");

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);


            var couponConn = conn.NewCoupon();
            var coupon = couponConn.Retrieve("alpha");

            Assert.True(coupon.Id == "alpha");
        }
        [Fact]
        public async Task TestRetrieveAsync()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("https://testmode/coupons/alpha")
                .Respond("application/json", "{'id' : 'alpha', 'name' : 'Alpha'}");

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);


            var couponConn = conn.NewCoupon();
            var coupon = await couponConn.RetrieveAsync("alpha");

            Assert.True(coupon.Id == "alpha");
        }


        [Fact]
        public void TestCreate()
        {
            var jsonResponse = @"{
				'created_at': 1574370277,
				'currency': null,
				'duration': 0,
				'exclusive': null,
				'expiration_date': null,
				'id': 'alpha',
				'is_percent': true,
				'max_redemptions': null,
				'metadata': {},
				'name': 'Alpha',
				'object': 'coupon',
				'value': 15
			}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://testmode/coupons")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var coupon = conn.NewCoupon();

            coupon.Create();

            Assert.True(coupon.Id == "alpha");
            Assert.True(coupon.Value == 15);
        }

        [Fact]
        public async Task TestCreateAsync()
        {
            var jsonResponse = @"{
				'created_at': 1574370277,
				'currency': null,
				'duration': 0,
				'exclusive': null,
				'expiration_date': null,
				'id': 'alpha',
				'is_percent': true,
				'max_redemptions': null,
				'metadata': {},
				'name': 'Alpha',
				'object': 'coupon',
				'value': 15
			}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://testmode/coupons")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var coupon = conn.NewCoupon();

            await coupon.CreateAsync();

            Assert.True(coupon.Id == "alpha");
            Assert.True(coupon.Value == 15);
        }

        [Fact]
        public void TestSave()
        {
            var jsonResponse = @"{
				'created_at': 1574370277,
				'currency': null,
				'duration': 0,
				'exclusive': null,
				'expiration_date': null,
				'id': 'alpha',
				'is_percent': true,
				'max_redemptions': null,
				'metadata': {},
				'name': 'Updated',
				'object': 'coupon',
				'value': 15
			}";


            var JsonRequest = @"{
                'name': 'Updated'
                }";

            var mockHttp = new MockHttpMessageHandler();
            var httpPatch = new HttpMethod("PATCH");
            var request = mockHttp.When(httpPatch, "https://testmode/coupons/alpha").WithJson(JsonRequest)
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var coupon = CreateDefaultCoupon(client);

            coupon.Name = "Updated";

            coupon.SaveAll();

            Assert.True(coupon.Id == "alpha");
            Assert.True(coupon.Name == "Updated");
        }

        [Fact]
        public async Task TestSaveAsync()
        {
            var jsonResponse = @"{
				'created_at': 1574370277,
				'currency': null,
				'duration': 0,
				'exclusive': null,
				'expiration_date': null,
				'id': 'alpha',
				'is_percent': true,
				'max_redemptions': null,
				'metadata': {},
				'name': 'Updated',
				'object': 'coupon',
				'value': 15
			}";


            var JsonRequest = @"{
                'name': 'Updated'
                }";

            var mockHttp = new MockHttpMessageHandler();
            var httpPatch = new HttpMethod("PATCH");
            var request = mockHttp.When(httpPatch, "https://testmode/coupons/alpha").WithJson(JsonRequest)
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var coupon = CreateDefaultCoupon(client);

            coupon.Name = "Updated";

            await coupon.SaveAllAsync();

            Assert.True(coupon.Id == "alpha");
            Assert.True(coupon.Name == "Updated");
        }

        [Fact]
        public void TestDelete()
        {
            var mockHttp = new MockHttpMessageHandler();

            var request = mockHttp.When(HttpMethod.Delete, "https://testmode/coupons/alpha")
                .Respond(HttpStatusCode.NoContent);

            var client = mockHttp.ToHttpClient();

            var coupon = CreateDefaultCoupon(client);

            coupon.Delete();
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            var mockHttp = new MockHttpMessageHandler();

            var request = mockHttp.When(HttpMethod.Delete, "https://testmode/coupons/alpha")
                .Respond(HttpStatusCode.NoContent);

            var client = mockHttp.ToHttpClient();

            var coupon = CreateDefaultCoupon(client);

            await coupon.DeleteAsync();
        }


        [Fact]
        public void TestListAll()
        {
            var jsonResponseListAll = @"[{
				'created_at': 1574370277,
				'currency': null,
				'duration': 0,
				'exclusive': null,
				'expiration_date': null,
				'id': 'alpha',
				'is_percent': true,
				'max_redemptions': null,
				'metadata': {},
				'name': 'Alpha',
				'object': 'coupon',
				'value': 15
			}]";

            var mockHttp = new MockHttpMessageHandler();

            var mockHeader = new Dictionary<string, string>();
            mockHeader["X-Total-Count"] = "1";
            mockHeader["Link"] =
                "<https://api.sandbox.invoiced.com/tax_rates?page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/tax_rates?page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/tax_rates?page=1>; rel=\"last\"";

            var request = mockHttp.When(HttpMethod.Get, "https://testmode/coupons")
                .Respond(mockHeader, "application/json", jsonResponseListAll);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var coupon = conn.NewCoupon();

            var coupons = coupon.ListAll();

            Assert.True(coupons[0].Id == "alpha");
        }
        [Fact]
        public async Task TestListAllAsync()
        {
            var jsonResponseListAll = @"[{
				'created_at': 1574370277,
				'currency': null,
				'duration': 0,
				'exclusive': null,
				'expiration_date': null,
				'id': 'alpha',
				'is_percent': true,
				'max_redemptions': null,
				'metadata': {},
				'name': 'Alpha',
				'object': 'coupon',
				'value': 15
			}]";

            var mockHttp = new MockHttpMessageHandler();

            var mockHeader = new Dictionary<string, string>();
            mockHeader["X-Total-Count"] = "1";
            mockHeader["Link"] =
                "<https://api.sandbox.invoiced.com/tax_rates?page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/tax_rates?page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/tax_rates?page=1>; rel=\"last\"";

            var request = mockHttp.When(HttpMethod.Get, "https://testmode/coupons")
                .Respond(mockHeader, "application/json", jsonResponseListAll);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var coupon = conn.NewCoupon();

            var coupons =await coupon.ListAllAsync();

            Assert.True(coupons[0].Id == "alpha");
        }
    }
}