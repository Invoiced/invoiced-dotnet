using System;
using Xunit;
using Invoiced;
using System.Net.Http;
using System.Collections.Generic;
using RichardSzalay.MockHttp;
using Newtonsoft.Json;


namespace InvoicedTest
{

    public class CustomerTest
    {

        [Fact]
        public void deserialize() {
           var json =  "{name : 'Test McGee'}";

           var customer = JsonConvert.DeserializeObject<Customer>(json);

            Console.WriteLine(customer);
        }


        // [Fact]
        // public void create()
        // {
        //     var mockHttp = new MockHttpMessageHandler();
        //     mockHttp.When("https://localhost:8080/customers/4")
        // .Respond("application/json", "{'id' : 4, 'name' : 'Test McGee'}");

        //     var client = mockHttp.ToHttpClient();

        //     Invoiced.Client.setHttpClientTest(client);

        //     var conn = new Connection("voodoo",Invoiced.Environment.local);

        //     var customerConn = conn.NewCustomer();

        //     var customer = customerConn.retrieve(4);

        //     Console.WriteLine(customer);

                 
        // }

        

    }
}
