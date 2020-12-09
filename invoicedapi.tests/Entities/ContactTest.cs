using System.Net;
using System.Net.Http;
using Invoiced;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Xunit;

namespace InvoicedTest
{
    public class ContactTest
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

        private static Contact CreateDefaultContact(HttpClient client)
        {
            var connection = new Connection("voodoo", Environment.test);
            connection.TestClient(client);

            var customer = CreateDefaultCustomer(client);
            var contact = customer.NewContact();
            contact.Id = 30699;

            contact.ChangeConnection(connection);

            return contact;
        }

        [Fact]
        public void TestDeserialize()
        {
            var json = @"{
                'address': '',
                'address1': null,
                'address2': null,
                'city': null,
                'country': 'US',
                'created_at': 1574199202,
                'department': null,
                'email': 'john@example.com',
                'id': 30699,
                'name': 'John Smith',
                'object': 'contact',
                'phone': null,
                'postal_code': null,
                'primary': true,
                'sms_enabled': false,
                'state': null,
                'title': null
            }";

            var contact = JsonConvert.DeserializeObject<Contact>(json);

            Assert.True(contact.Id == 30699);
            Assert.True(contact.Name == "John Smith");
        }


        [Fact]
        public void TestRetrieve()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("https://testmode/customers/1234/contacts/30699")
                .Respond("application/json", "{'id' : 30699, 'name' : 'Test McGee'}");

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var customer = CreateDefaultCustomer(client);

            var contact = customer.NewContact().Retrieve(30699);

            Assert.True(contact.Id == 30699);
            Assert.True(contact.Name == "Test McGee");
        }


        [Fact]
        public void TestCreate()
        {
            var jsonResponse = @"{
                'address': '',
                'address1': null,
                'address2': null,
                'city': null,
                'country': 'US',
                'created_at': 1574199202,
                'department': null,
                'email': 'john@example.com',
                'id': 30699,
                'name': 'John Smith',
                'object': 'contact',
                'phone': null,
                'postal_code': null,
                'primary': true,
                'sms_enabled': false,
                'state': null,
                'title': null
            }";


            var jsonRequest = @"{
                'name': 'John Smith',
                'email': 'john@example.com'
                }";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://testmode/customers/1234/contacts").WithJson(jsonRequest)
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var customer = CreateDefaultCustomer(client);
            var contact = customer.NewContact();

            contact.Name = "John Smith";
            contact.Email = "john@example.com";
            contact.Create();

            Assert.True(contact.Id == 30699);
        }

        [Fact]
        public void TestSave()
        {
            var jsonResponse = @"{
                'address': '',
                'address1': null,
                'address2': null,
                'city': null,
                'country': 'US',
                'created_at': 1574199202,
                'department': null,
                'email': 'john@example.com',
                'id': 30699,
                'name': 'John Smith',
                'object': 'contact',
                'phone': null,
                'postal_code': null,
                'primary': true,
                'sms_enabled': true,
                'state': null,
                'title': null
            }";


            var jsonRequest = @"{
                'sms_enabled': true
                }";

            var mockHttp = new MockHttpMessageHandler();
            var httpPatch = new HttpMethod("PATCH");
            var request = mockHttp.When(httpPatch, "https://testmode/customers/1234/contacts/30699")
                .WithJson(jsonRequest).Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var contact = CreateDefaultContact(client);

            contact.SmsEnabled = true;
            contact.SaveAll();

            Assert.True(contact.SmsEnabled);
        }

        [Fact]
        public void TestDelete()
        {
            var mockHttp = new MockHttpMessageHandler();

            var request = mockHttp.When(HttpMethod.Delete, "https://testmode/customers/1234/contacts/30699")
                .Respond(HttpStatusCode.NoContent);

            var client = mockHttp.ToHttpClient();

            var contact = CreateDefaultContact(client);

            contact.Delete();
        }
    }
}