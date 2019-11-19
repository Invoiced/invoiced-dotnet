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

    public class CustomerTest
    {
      private Customer createDefaultCustomer(HttpClient client) {
           var json =  @"{'id': 594287
                }";

            var customer = JsonConvert.DeserializeObject<Customer>(json);

            var connection  = new Connection("voodoo",Invoiced.Environment.test);

            connection.TestClient(client);

            customer.ChangeConnection(connection);

            return customer;

      }

        [Fact]
        public void Deserialize() {
           var json =  @"{'id': 594287,
                'object': 'customer',
                'name': 'Acme',
                'number': 'CUST-0001',
                'email': 'billing@acmecorp.com',
                'autopay': true,
                'payment_terms': null,
                'payment_source': {
                    'id': 850,
                    'object': 'card',
                    'brand': 'Visa',
                    'last4': '4242',
                    'exp_month': 2,
                    'exp_year': 20,
                    'funding': 'credit'
                },
                'taxes': [],
                'type': 'company',
                'attention_to': null,
                'address1': null,
                'address2': null,
                'city': null,
                'state': null,
                'postal_code': null,
                'country': 'US',
                'tax_id': null,
                'phone': null,
                'notes': null,
                'sign_up_page': null,
                'sign_up_url': null,
                'statement_pdf_url': 'https://dundermifflin.invoiced.com/statements/t3NmhUomra3g3ueSNnbtUgrr/pdf',
                'created_at': 1415222128,
                'metadata': {}
                }";

           var customer = JsonConvert.DeserializeObject<Customer>(json);

            Assert.True(customer.Number == "CUST-0001");
            Assert.True(customer.Name == "Acme");
            Assert.True(customer.Email == "billing@acmecorp.com");
        }


        [Fact]
        public void Retrieve()
        {

            var mockHttp = new MockHttpMessageHandler();
           
            mockHttp.When("https://testmode/customers/4").Respond("application/json", "{'id' : 4, 'name' : 'Test McGee'}");
   
            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo",Invoiced.Environment.test);
           
            conn.TestClient(client);

            var customerConn = conn.NewCustomer();

            var customer = customerConn.Retrieve(4);

            Assert.True(customer.Id == 4);
    
        }


        [Fact]
        public void Create()
        {

             var  jsonReponseSave = @"{
            'address1': '123 Main St',
            'address2': null,
            'attention_to': null,
            'autopay': false,
            'autopay_delay_days': -1,
            'avalara_entity_use_code': null,
            'avalara_exemption_number': null,
            'bill_to_parent': null,
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
            'metadata': {},
            'name': 'Acme',
            'next_chase_step': null,
            'notes': null,
            'number': 'CUST-00006',
            'object': 'customer',
            'owner': 1976,
            'parent_customer': null,
            'payment_source': null,
            'payment_terms': 'NET 7',
            'phone': '5125551212',
            'postal_code': '78730',
            'sign_up_page': null,
            'sign_up_url': null,
            'state': 'TX',
            'statement_pdf_url': 'https://ajwt.sandbox.invoiced.com/statements/iqNxncYsm91S3gtpz0jtzEXY/pdf',
            'tax_id': null,
            'taxable': true,
            'taxes': [],
            'type': 'company'
        }";


            var jsonRequest = @"{
                'name': 'Acme',
                'email': 'example@example.com',
                'payment_terms': 'NET 7',
                'type': 'company'
                }";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post,"https://testmode/customers").WithJson(jsonRequest).Respond("application/json",jsonReponseSave);
     
            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo",Invoiced.Environment.test);
           
            conn.TestClient(client);

            var customer = conn.NewCustomer();

            customer.Name = "Acme";
            customer.Email = "example@example.com";
            customer.PaymentTerms = "NET 7";
            customer.Type = "company";
            customer.Create();

            Assert.True(customer.Id == 594287);
    
        }

        [Fact]
        public void Save()
        {

        var  jsonReponseSave = @"{
            'address1': '123 Main St',
            'address2': null,
            'attention_to': 'Sarah Fisher',
            'autopay': false,
            'autopay_delay_days': -1,
            'avalara_entity_use_code': null,
            'avalara_exemption_number': null,
            'bill_to_parent': null,
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
            'metadata': {},
            'name': 'Acme',
            'next_chase_step': null,
            'notes': null,
            'number': 'CUST-00006',
            'object': 'customer',
            'owner': 1976,
            'parent_customer': null,
            'payment_source': null,
            'payment_terms': 'NET 7',
            'phone': '5125551212',
            'postal_code': '78730',
            'sign_up_page': null,
            'sign_up_url': null,
            'state': 'TX',
            'statement_pdf_url': 'https://ajwt.sandbox.invoiced.com/statements/iqNxncYsm91S3gtpz0jtzEXY/pdf',
            'tax_id': null,
            'taxable': true,
            'taxes': [],
            'type': 'company'
        }";


            var JsonRequest = @"{
                'attention_to': 'Sarah Fisher'
                }";

            var mockHttp = new MockHttpMessageHandler();
            var httpPatch = new HttpMethod("PATCH");
            var request = mockHttp.When(httpPatch,"https://testmode/customers/594287").WithJson(JsonRequest).Respond("application/json",jsonReponseSave);

            var client = mockHttp.ToHttpClient();

            var customer = createDefaultCustomer(client);

            customer.AttentionTo = "Sarah Fisher";
 
            customer.SaveAll();
          
            Assert.True(customer.Id == 594287);
            Assert.True(customer.Number == "CUST-00006");
            Assert.True(customer.AttentionTo == "Sarah Fisher");

        } 

        [Fact]
        public void delete() {

            var mockHttp = new MockHttpMessageHandler();
  
            var request = mockHttp.When(HttpMethod.Delete,"https://testmode/customers/594287").Respond(HttpStatusCode.NoContent);

            var client = mockHttp.ToHttpClient();

            var customer = createDefaultCustomer(client);

            customer.Delete();

        } 


        [Fact]
        public void listall() {

            var  jsonReponseListall = @"[{
            'id': 15444,
            'object': 'customer',
            'number': 'CUST-0001',
            'name': 'Acme',
            'email': 'billing@acmecorp.com',
            'autopay': false,
            'payment_terms': 'NET 14',
            'payment_source': null,
            'taxes': [],
            'type': 'company',
            'attention_to': 'Sarah Fisher',
            'address1': '342 Amber St',
            'address2': null,
            'city': 'Hill Valley',
            'state': 'CA',
            'postal_code': '94523',
            'country': 'US',
            'tax_id': '893-934835',
            'phone': '(820) 297-2983',
            'notes': null,
            'sign_up_page': null,
            'sign_up_url': null,
            'statement_pdf_url': 'https://dundermifflin.invoiced.com/statements/t3NmhUomra3g3ueSNnbtUgrr/pdf',
            'created_at': 1415222128,
            'metadata': {}
            }]";

            var mockHttp = new MockHttpMessageHandler();

            var filterByNameQ = new Dictionary<string, string>{ {"filter[name]", "Abraham Lincoln"}};

            var filterByName = new Dictionary<string, Object>{ {"filter[name]", "Abraham Lincoln"}};

            var mockHeader = new Dictionary<string,string>();
            mockHeader["X-Total-Count"] = "1";
            mockHeader["Link"] = "<https://api.sandbox.invoiced.com/customers?filter%5Bname%5D=Abraham+Lincoln&page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/customers?filter%5Bname%5D=Abraham+Lincoln&page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/customers?filter%5Bname%5D=Abraham+Lincoln&page=1>; rel=\"last\"";
   
            var request = mockHttp.When(HttpMethod.Get,"https://testmode/customers").WithExactQueryString(filterByNameQ).Respond(mockHeader,"application/json",jsonReponseListall);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo",Invoiced.Environment.test);
           
            conn.TestClient(client);

            var customer = conn.NewCustomer();

            var customers = customer.ListAll(filterByName);

            Assert.True(customers[0].Id == 15444);

        }   

        
           

    }
}
