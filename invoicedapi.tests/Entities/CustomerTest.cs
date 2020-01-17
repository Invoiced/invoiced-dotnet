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
      private static Customer CreateDefaultCustomer(HttpClient client) {
           var json =  @"{'id': 1234
                }";

            var customer = JsonConvert.DeserializeObject<Customer>(json);

            var connection  = new Connection("voodoo",Invoiced.Environment.test);

            connection.TestClient(client);

            customer.ChangeConnection(connection);

            return customer;

      }

        [Fact]
        public void TestDeserialize() {
           var json =  @"{'id': 1234,
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
        public void TestRetrieve()
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
        public void TestCreate()
        {

             var  jsonResponse = @"{
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
            'id': 1234,
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

            mockHttp.When(HttpMethod.Post,"https://testmode/customers").WithJson(jsonRequest).Respond("application/json",jsonResponse);
     
            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo",Invoiced.Environment.test);
           
            conn.TestClient(client);

            var customer = conn.NewCustomer();

            customer.Name = "Acme";
            customer.Email = "example@example.com";
            customer.PaymentTerms = "NET 7";
            customer.Type = "company";
            customer.Create();

            Assert.True(customer.Id == 1234);
    
        }

        [Fact]
        public void TestSave()
        {

        var  jsonResponse = @"{
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
            'id': 1234,
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
            var request = mockHttp.When(httpPatch,"https://testmode/customers/1234").WithJson(JsonRequest).Respond("application/json",jsonResponse);

            var client = mockHttp.ToHttpClient();

            var customer = CreateDefaultCustomer(client);

            customer.AttentionTo = "Sarah Fisher";
 
            customer.SaveAll();
          
            Assert.True(customer.Id == 1234);
            Assert.True(customer.Number == "CUST-00006");
            Assert.True(customer.AttentionTo == "Sarah Fisher");

        } 

        [Fact]
        public void TestDelete() {

            var mockHttp = new MockHttpMessageHandler();
  
            var request = mockHttp.When(HttpMethod.Delete,"https://testmode/customers/1234").Respond(HttpStatusCode.NoContent);

            var client = mockHttp.ToHttpClient();

            var customer = CreateDefaultCustomer(client);

            customer.Delete();

        } 


        [Fact]
        public void TestListAll() {

            var  jsonResponseListAll = @"[{
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
   
            var request = mockHttp.When(HttpMethod.Get,"https://testmode/customers").WithExactQueryString(filterByNameQ).Respond(mockHeader,"application/json",jsonResponseListAll);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo",Invoiced.Environment.test);
           
            conn.TestClient(client);

            var customer = conn.NewCustomer();

            var customers = customer.ListAll(filterByName);

            Assert.True(customers[0].Id == 15444);

        }   

        [Fact]
        public void TestNewNote()
        {

            var jsonResponse = @"{'customer_id':1234,'id':1212,'notes':'example note',
            'object': 'note'
            }";


            var jsonRequest = @"{
                'customer_id': 1234,
                'notes': 'example note'
                }";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post,"https://testmode/notes").WithJson(jsonRequest).Respond("application/json",jsonResponse);
     
            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo",Invoiced.Environment.test);
           
            conn.TestClient(client);

            var customer = CreateDefaultCustomer(client);

            var testNote = customer.NewNote();
            testNote.Notes = "example note";
            testNote.Create();

            Assert.True(testNote.Obj == "note");
            Assert.True(testNote.Id == 1212);
    
        }
        
        [Fact]
        public void TestNewContact()
        {

            const string jsonResponse = @"{
	            'name': 'John Smith',
	            'email': 'john@example.com',
	            'sms_enabled': false,
	            'primary': true,
	            'country': 'US'
            }";


            const string jsonRequest = @"{
                'name': 'John Smith',
                }";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post,"https://testmode/customers/1234/contacts").WithJson(jsonRequest).Respond("application/json",jsonResponse);
     
            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo",Invoiced.Environment.test);
           
            conn.TestClient(client);

            var customer = CreateDefaultCustomer(client);

            var testContact = customer.NewContact();
            testContact.Name = "John Smith";
            testContact.Create();

            Assert.True(testContact.GetEndpoint(false) == "/customers/1234/contacts");
            Assert.True(testContact.Name == "John Smith");
    
        }
        
        [Fact]
        public void TestNewPli()
        {

            const string jsonResponse = @"{
	            'amount': 100,
	            'catalog_item': null,
	            'created_at': 1574199627,
	            'customer': 1234,
	            'description': '',
	            'discountable': true,
	            'discounts': [],
	            'id': 22904406,
	            'metadata': {},
	            'name': 'Paper',
	            'object': 'line_item',
	            'quantity': 1,
	            'taxable': true,
	            'taxes': [],
	            'type': null,
	            'unit_cost': 100
            }";


            const string jsonRequest = @"{
                'name': 'Paper',
                'quantity': 1.0,
                'unit_cost': 100.0
                }";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post,"https://testmode/customers/1234/line_items").WithJson(jsonRequest).Respond("application/json",jsonResponse);
     
            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo",Invoiced.Environment.test);
           
            conn.TestClient(client);

            var customer = CreateDefaultCustomer(client);

            var testPli = customer.NewPendingLineItem();
            testPli.Name = "Paper";
            testPli.Quantity = 1;
            testPli.UnitCost = 100;
            testPli.Create();

            Assert.True(testPli.GetEndpoint(false) == "/customers/1234/line_items");
            Assert.True(testPli.Id == 22904406);
    
        }
        
        [Fact]
        public void TestNewTask()
        {

            const string jsonResponse = @"{
              'action': 'review',
              'chase_step_id': null,
              'complete': false,
              'completed_by_user_id': null,
              'completed_date': null,
              'created_at': 1571347283,
              'customer_id': 1234,
              'due_date': 1571288400,
              'id': 788,
              'name': 'Example',
              'user_id': null
            }";


            const string jsonRequest = @"{
                'name': 'Example',
                'customer_id': 1234,
                'action': 'review'
                }";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post,"https://testmode/tasks").WithJson(jsonRequest).Respond("application/json",jsonResponse);
     
            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo",Invoiced.Environment.test);
           
            conn.TestClient(client);

            var customer = CreateDefaultCustomer(client);

            var testTask = customer.NewTask();
            testTask.Name = "Example";
            testTask.Action = "review";
            testTask.Create();

            Assert.True(testTask.GetEndpoint(false) == "/tasks");
            Assert.True(testTask.Id == 788);
    
        }
        [Fact]
        public void TestSendEmail()
        {

            const string jsonResponse = @"[{'id':'f45382c6fbc44d44aa7f9a55eb2ce731',
            'state':'sent',
            'reject_reason':null,
            'email':'client@example.com',
            'template':'statement_email',
            'subject':'test case',
            'message':'test',
            'opens':0,
            'opens_detail':[],
            'clicks':0,
            'clicks_detail':[],
            'created_at':1436890047}]";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post,"https://testmode/customers/1234/emails").Respond("application/json",jsonResponse);
     
            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo",Invoiced.Environment.test);
           
            conn.TestClient(client);

            var customer = CreateDefaultCustomer(client);
            var testRequest = new EmailRequest();
            var response = customer.SendStatementEmail(testRequest);

            Assert.True(response[0].State == "sent");
            Assert.True(response.Count == 1);

        }
        
        [Fact]
        public void TestSendLetter()
        {

            const string jsonResponse = @"{
              'created_at': 1570826337,
              'expected_delivery_date': 1571776737,
              'id': '2678c1e7e6dd1011ce13fb6b76db42df',
              'num_pages': 1,
              'state': 'queued',
              'to': 'Acme Inc.\n5301 Southwest Pkwy\nAustin, TX 78735'
            }";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post,"https://testmode/customers/1234/letters").Respond("application/json",jsonResponse);
     
            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo",Invoiced.Environment.test);
           
            conn.TestClient(client);

            var customer = CreateDefaultCustomer(client);
            var testRequest = new LetterRequest();
            var response = customer.SendStatementLetter(testRequest);

            Assert.True(response.State == "queued");

        }
        
        [Fact]
        public void TestSendText()
        {

            const string jsonResponse = @"[{
                'created_at': 1571086718,
                'id': 'c05c9cae8c5799da1e5723a0fff355b3',
                'message': 'test',
                'state': 'sent',
                'to': '+15125551212'
              }
            ]";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post,"https://testmode/customers/1234/text_messages").Respond("application/json",jsonResponse);
     
            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo",Invoiced.Environment.test);
           
            conn.TestClient(client);

            var customer = CreateDefaultCustomer(client);
            var testRequest = new TextRequest();
            var response = customer.SendStatementText(testRequest);

            Assert.True(response[0].State == "sent");
            Assert.True(response[0].To == "+15125551212");
            Assert.True(response.Count == 1);

        }
        
        [Fact]
        public void TestGetBalance()
        {

            const string jsonResponse = @"{
                'available_credits':50,
                'history': [
                    {
                        'timestamp':1464041624,
                        'balance': 50
                    },
                    {
                        'timestamp': 1464040550,
                        'balance': 100
                    }
                ],
                'past_due': false,
                'total_outstanding': 470
            }";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Get,"https://testmode/customers/1234/balance").Respond("application/json",jsonResponse);
     
            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo",Invoiced.Environment.test);
           
            conn.TestClient(client);

            var customer = CreateDefaultCustomer(client);
            var response = customer.GetBalance();

            Assert.True(response.AvailableCredits == 50);
            Assert.True(response.History.Count == 2);

        }
        
        [Fact]
        public void TestConsolidateInvoices()
        {

            const string jsonResponse = @"{'id': 46226,'customer': 1234}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post,"https://testmode/customers/1234/consolidate_invoices").Respond("application/json",jsonResponse);
     
            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo",Invoiced.Environment.test);
           
            conn.TestClient(client);

            var customer = CreateDefaultCustomer(client);
            var response = customer.ConsolidateInvoices();

            Assert.True(response.Id == 46226);
            Assert.True(response.Customer == 1234);

        }
    
    }
    
}
