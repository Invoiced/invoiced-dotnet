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

    public class NoteTest
    {
      private static Customer CreateDefaultCustomer(HttpClient client) {
           var json =  @"{'id': 1234
                }";

            var customer = JsonConvert.DeserializeObject<Customer>(json);

            var connection  = new Connection("voodoo",Invoiced.Environment.test);

            connection.TestClient(client);

            customer.ChangeConnection(connection);

            return customer;

      }
      
      private static Invoice CreateDefaultInvoice(HttpClient client) {
          var json =  @"{'id': 56789
                }";

          var invoice = JsonConvert.DeserializeObject<Invoice>(json);

          var connection  = new Connection("voodoo",Invoiced.Environment.test);

          connection.TestClient(client);

          invoice.ChangeConnection(connection);

          return invoice;

      }
      
      private static Note CreateDefaultNote(HttpClient client) {
          
          var connection  = new Connection("voodoo",Invoiced.Environment.test);
          connection.TestClient(client);
          
          var customer = CreateDefaultCustomer(client);
          var note = customer.NewNote();
          note.Notes = "example note";
          note.Id = 1212;

          note.ChangeConnection(connection);

          return note;

      }

        [Fact]
        public void TestDeserialize() {
           var json =  @"{
	            'created_at': 1574266753,
	            'customer': 1234,
	            'id': 1212,
	            'notes': 'test',
	            'object': 'note',
	            'user': {
		            'created_at': 1563810757,
		            'email': 'example@example.com',
		            'first_name': 'John',
		            'id': 1976,
		            'last_name': 'Smith',
		            'two_factor_enabled': false,
		            'registered': true
	            }
            }";

           var note = JsonConvert.DeserializeObject<Note>(json);

            Assert.True(note.Id == 1212);
            Assert.True(note.Notes == "test");
        }


        [Fact]
                 public void TestRetrieveCustNote()
                 {
         
                     var mockHttp = new MockHttpMessageHandler();
                     
                     var mockHeader = new Dictionary<string,string>();
                     mockHeader["X-Total-Count"] = "1";
                     mockHeader["Link"] = "<https://api.sandbox.invoiced.com/customers/1234/notes&page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/customers/1234/notes&page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/customers/1234/notes&page=1>; rel=\"last\"";
                    
                     mockHttp.When(HttpMethod.Get,"https://testmode/customers/1234/notes").Respond(mockHeader, "application/json", "[{'id' : 1212, 'notes' : 'Test McGee'}]");
            
                     var client = mockHttp.ToHttpClient();
         
                     var conn = new Connection("voodoo",Invoiced.Environment.test);
                    
                     conn.TestClient(client);
         
                     var customer = CreateDefaultCustomer(client);
         
                     var note = customer.NewNote().Retrieve();
         
                     Assert.True(note[0].Id == 1212);
                     Assert.True(note[0].Notes == "Test McGee");
                     Assert.True(note.Count == 1);
             
                 }
                 
                 [Fact]
                 public void TestRetrieveInvNote()
                 {

                     var mockHttp = new MockHttpMessageHandler();
            
                     var mockHeader = new Dictionary<string,string>();
                     mockHeader["X-Total-Count"] = "1";
                     mockHeader["Link"] = "<https://api.sandbox.invoiced.com/invoices/56789/notes&page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/invoices/56789/notes&page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/invoices/56789/notes&page=1>; rel=\"last\"";
           
                     mockHttp.When(HttpMethod.Get,"https://testmode/invoices/56789/notes").Respond(mockHeader, "application/json", "[{'id' : 2121, 'notes' : 'Test McGee'}]");
   
                     var client = mockHttp.ToHttpClient();

                     var conn = new Connection("voodoo",Invoiced.Environment.test);
           
                     conn.TestClient(client);

                     var invoice = CreateDefaultInvoice(client);

                     var note = invoice.NewNote().Retrieve();

                     Assert.True(note[0].Id == 2121);
                     Assert.True(note[0].Notes == "Test McGee");
                     Assert.True(note.Count == 1);
    
                 }


        [Fact]
        public void TestCreate()
        {

             var  jsonResponse = @"{
	            'created_at': 1574266753,
	            'customer': 1234,
	            'id': 1212,
	            'notes': 'test',
	            'object': 'note',
	            'user': {
		            'created_at': 1563810757,
		            'email': 'example@example.com',
		            'first_name': 'John',
		            'id': 1976,
		            'last_name': 'Smith',
		            'two_factor_enabled': false,
		            'registered': true
	            }
            }";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post,"https://testmode/notes").Respond("application/json",jsonResponse);
     
            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo",Invoiced.Environment.test);
           
            conn.TestClient(client);

            var customer = CreateDefaultCustomer(client);
            var note = customer.NewNote();
            
            note.Notes = "example";
            note.Create();

            Assert.True(note.Id == 1212);
    
        }

        [Fact]
        public void TestSave()
        {

        var  jsonResponse = @"{
	            'created_at': 1574266753,
	            'customer': 1234,
	            'id': 1212,
	            'notes': 'test2',
	            'object': 'note',
	            'user': {
		            'created_at': 1563810757,
		            'email': 'example@example.com',
		            'first_name': 'John',
		            'id': 1976,
		            'last_name': 'Smith',
		            'two_factor_enabled': false,
		            'registered': true
	            }
            }";


            var jsonRequest = @"{
                'notes': 'test2'
                }";

            var mockHttp = new MockHttpMessageHandler();
            var httpPatch = new HttpMethod("PATCH");
            var request = mockHttp.When(httpPatch,"https://testmode/notes/1212").WithJson(jsonRequest).Respond("application/json",jsonResponse);
     
            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo",Invoiced.Environment.test);
           
            conn.TestClient(client);

            var note = CreateDefaultNote(client);

            note.Notes = "test2";
            note.SaveAll();

            Assert.True(note.Notes == "test2");

        } 

        [Fact]
        public void TestDelete() {

            var mockHttp = new MockHttpMessageHandler();
  
            var request = mockHttp.When(HttpMethod.Delete,"https://testmode/notes/1212").Respond(HttpStatusCode.NoContent);

            var client = mockHttp.ToHttpClient();

            var note = CreateDefaultNote(client);

            note.Delete();

        }

    }
    
}
