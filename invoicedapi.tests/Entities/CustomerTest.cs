using System;
using Xunit;
using Invoiced;
using System.Net.Http;
using System.Collections.Generic;
using RichardSzalay.MockHttp;
using Newtonsoft.Json;


namespace InvoicedTesta
{

    public class CustomerTest
    {

        [Fact]
        public void Deserialize() {
           var json =  @"{'id': 15444,
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
        public void Create()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://localhost:8080/customers/4")
        .Respond("application/json", "{'id' : 4, 'name' : 'Test McGee'}");

            var client = mockHttp.ToHttpClient();

            Invoiced.Client.SetHttpClientTest(client);

            var conn = new Connection("voodoo",Invoiced.Environment.local);

            var customerConn = conn.NewCustomer();

            var customer = customerConn.Retrieve(4);
            Console.WriteLine(customer);
            Assert.True(customer.Id == 4);

        }

        

    }
}
