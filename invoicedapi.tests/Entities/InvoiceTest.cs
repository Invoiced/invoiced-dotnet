using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Invoiced;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Xunit;

namespace InvoicedTest
{
    public class InvoiceTest
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

        private static Invoice CreateDefaultInvoice(HttpClient client)
        {
            var json = @"{'id': 2334745
                }";

            var invoice = JsonConvert.DeserializeObject<Invoice>(json);

            var connection = new Connection("voodoo", Environment.test);

            connection.TestClient(client);

            invoice.ChangeConnection(connection);

            return invoice;
        }

        [Fact]
        public void TestDeserialize()
        {
            var json = @"{
            'attempt_count': 4,
            'autopay': true,
            'balance': 174,
            'chase': false,
            'closed': false,
            'created_at': 1572971872,
            'csv_url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi\/csv',
            'currency': 'usd',
            'customer': 576126,
            'date': 1370062800,
            'discounts': [
                {
                    'amount': 2,
                    'expires': null,
                    'id': 2115753,
                    'object': 'discount',
                    'coupon': null
                }
            ],
            'draft': false,
            'due_date': null,
            'id': 2334745,
            'items': [
                {
                    'amount': 120,
                    'catalog_item': null,
                    'created_at': 1572971873,
                    'description': '',
                    'discountable': true,
                    'id': 22828070,
                    'name': 'Doctor\'s Visit',
                    'quantity': 3,
                    'taxable': true,
                    'type': null,
                    'unit_cost': 40,
                    'object': 'line_item',
                    'metadata': {},
                    'discounts': [],
                    'taxes': []
                },
                {
                    'amount': 46,
                    'catalog_item': null,
                    'created_at': 1572971873,
                    'description': '',
                    'discountable': true,
                    'id': 22828071,
                    'name': 'Supplies',
                    'quantity': 1,
                    'taxable': true,
                    'type': null,
                    'unit_cost': 46,
                    'object': 'line_item',
                    'metadata': {},
                    'discounts': [],
                    'taxes': []
                }
            ],
            'metadata': {},
            'name': 'Invoice',
            'needs_attention': false,
            'next_chase_on': null,
            'next_payment_attempt': null,
            'notes': null,
            'number': 'INV-0003',
            'object': 'invoice',
            'paid': false,
            'payment_plan': null,
            'payment_source': null,
            'payment_terms': 'AutoPay',
            'payment_url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi\/payment',
            'pdf_url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi\/pdf',
            'purchase_order': null,
            'ship_to': {
                'address1': '1234 Main St',
                'address2': null,
                'attention_to': null,
                'city': 'Austin',
                'country': 'US',
                'created_at': 1574272634,
                'name': 'Dr. Watson',
                'postal_code': '78704',
                'state': 'TX'
            },
            'shipping': [],
            'status': 'past_due',
            'subscription': null,
            'subtotal': 166,
            'taxes': [
                {
                    'amount': 10,
                    'id': 2115754,
                    'object': 'tax',
                    'tax_rate': null
                }
            ],
            'total': 174,
            'url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi'
        }";

            var invoice = JsonConvert.DeserializeObject<Invoice>(json);

            Assert.True(invoice.Id == 2334745);
            Assert.True(invoice.Name == "Invoice");
            Assert.True(invoice.Subtotal == 166);
        }


        [Fact]
        public void TestRetrieve()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("https://api.testmode.com/invoices/4")
                .Respond("application/json", "{'id' : 4, 'number' : 'INV-0004'}");

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var invoiceConn = conn.NewInvoice();

            var invoice = invoiceConn.Retrieve(4);

            Assert.True(invoice.Number == "INV-0004");
        }


        [Fact]
        public void TestCreate()
        {
            var jsonResponse = @"{
                    'attempt_count': 4,
                    'autopay': true,
                    'balance': 174,
                    'chase': false,
                    'closed': false,
                    'created_at': 1572971872,
                    'csv_url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi\/csv',
                    'currency': 'usd',
                    'customer': 576126,
                    'date': 1370062800,
                    'discounts': [
                        {
                            'amount': 2,
                            'expires': null,
                            'id': 2115753,
                            'object': 'discount',
                            'coupon': null
                        }
                    ],
                    'draft': false,
                    'due_date': null,
                    'id': 2334745,
                    'items': [
                        {
                            'amount': 120,
                            'catalog_item': null,
                            'created_at': 1572971873,
                            'description': '',
                            'discountable': true,
                            'id': 22828070,
                            'name': 'Doctor\'s Visit',
                            'quantity': 3,
                            'taxable': true,
                            'type': null,
                            'unit_cost': 40,
                            'object': 'line_item',
                            'metadata': {},
                            'discounts': [],
                            'taxes': []
                        },
                        {
                            'amount': 46,
                            'catalog_item': null,
                            'created_at': 1572971873,
                            'description': '',
                            'discountable': true,
                            'id': 22828071,
                            'name': 'Supplies',
                            'quantity': 1,
                            'taxable': true,
                            'type': null,
                            'unit_cost': 46,
                            'object': 'line_item',
                            'metadata': {},
                            'discounts': [],
                            'taxes': []
                        }
                    ],
                    'metadata': {},
                    'name': 'Invoice',
                    'needs_attention': false,
                    'next_chase_on': null,
                    'next_payment_attempt': null,
                    'notes': null,
                    'number': 'INV-0003',
                    'object': 'invoice',
                    'paid': false,
                    'payment_plan': null,
                    'payment_source': null,
                    'payment_terms': 'AutoPay',
                    'payment_url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi\/payment',
                    'pdf_url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi\/pdf',
                    'purchase_order': null,
                    'ship_to': {
                        'address1': '1234 Main St',
                        'address2': null,
                        'attention_to': null,
                        'city': 'Austin',
                        'country': 'US',
                        'created_at': 1574272634,
                        'name': 'Dr. Watson',
                        'postal_code': '78704',
                        'state': 'TX'
                    },
                    'shipping': [],
                    'status': 'past_due',
                    'subscription': null,
                    'subtotal': 166,
                    'taxes': [
                        {
                            'amount': 10,
                            'id': 2115754,
                            'object': 'tax',
                            'tax_rate': null
                        }
                    ],
                    'total': 174,
                    'url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi'
                }";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://api.testmode.com/invoices").Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var invoice = conn.NewInvoice();

            invoice.Create();

            Assert.True(invoice.Id == 2334745);
        }

        [Fact]
        public void TestSave()
        {
            var jsonResponse = @"{
                    'attempt_count': 4,
                    'autopay': true,
                    'balance': 174,
                    'chase': false,
                    'closed': false,
                    'created_at': 1572971872,
                    'csv_url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi\/csv',
                    'currency': 'usd',
                    'customer': 576126,
                    'date': 1370062800,
                    'discounts': [
                        {
                            'amount': 2,
                            'expires': null,
                            'id': 2115753,
                            'object': 'discount',
                            'coupon': null
                        }
                    ],
                    'draft': false,
                    'due_date': null,
                    'id': 2334745,
                    'items': [
                        {
                            'amount': 120,
                            'catalog_item': null,
                            'created_at': 1572971873,
                            'description': '',
                            'discountable': true,
                            'id': 22828070,
                            'name': 'Doctor\'s Visit',
                            'quantity': 3,
                            'taxable': true,
                            'type': null,
                            'unit_cost': 40,
                            'object': 'line_item',
                            'metadata': {},
                            'discounts': [],
                            'taxes': []
                        },
                        {
                            'amount': 46,
                            'catalog_item': null,
                            'created_at': 1572971873,
                            'description': '',
                            'discountable': true,
                            'id': 22828071,
                            'name': 'Supplies',
                            'quantity': 1,
                            'taxable': true,
                            'type': null,
                            'unit_cost': 46,
                            'object': 'line_item',
                            'metadata': {},
                            'discounts': [],
                            'taxes': []
                        }
                    ],
                    'metadata': {},
                    'name': 'Updated',
                    'needs_attention': false,
                    'next_chase_on': null,
                    'next_payment_attempt': null,
                    'notes': null,
                    'number': 'INV-0003',
                    'object': 'invoice',
                    'paid': false,
                    'payment_plan': null,
                    'payment_source': null,
                    'payment_terms': 'AutoPay',
                    'payment_url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi\/payment',
                    'pdf_url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi\/pdf',
                    'purchase_order': null,
                    'ship_to': {
                        'address1': '1234 Main St',
                        'address2': null,
                        'attention_to': null,
                        'city': 'Austin',
                        'country': 'US',
                        'created_at': 1574272634,
                        'name': 'Dr. Watson',
                        'postal_code': '78704',
                        'state': 'TX'
                    },
                    'shipping': [],
                    'status': 'past_due',
                    'subscription': null,
                    'subtotal': 166,
                    'taxes': [
                        {
                            'amount': 10,
                            'id': 2115754,
                            'object': 'tax',
                            'tax_rate': null
                        }
                    ],
                    'total': 174,
                    'url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi'
                }";

            var mockHttp = new MockHttpMessageHandler();
            var httpPatch = new HttpMethod("PATCH");
            var request = mockHttp.When(httpPatch, "https://api.testmode.com/invoices/2334745")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var invoice = CreateDefaultInvoice(client);

            invoice.Name = "Updated";

            invoice.SaveAll();

            Assert.True(invoice.Name == "Updated");
        }

        [Fact]
        public void TestDelete()
        {
            var mockHttp = new MockHttpMessageHandler();

            var request = mockHttp.When(HttpMethod.Delete, "https://api.testmode.com/invoices/2334745")
                .Respond(HttpStatusCode.NoContent);

            var client = mockHttp.ToHttpClient();

            var customer = CreateDefaultInvoice(client);

            customer.Delete();
        }


        [Fact]
        public void TestListAll()
        {
            var jsonResponseListAll = @"[{
                    'attempt_count': 4,
                    'autopay': true,
                    'balance': 174,
                    'chase': false,
                    'closed': false,
                    'created_at': 1572971872,
                    'csv_url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi\/csv',
                    'currency': 'usd',
                    'customer': 576126,
                    'date': 1370062800,
                    'discounts': [
                        {
                            'amount': 2,
                            'expires': null,
                            'id': 2115753,
                            'object': 'discount',
                            'coupon': null
                        }
                    ],
                    'draft': false,
                    'due_date': null,
                    'id': 2334745,
                    'items': [
                        {
                            'amount': 120,
                            'catalog_item': null,
                            'created_at': 1572971873,
                            'description': '',
                            'discountable': true,
                            'id': 22828070,
                            'name': 'Doctor\'s Visit',
                            'quantity': 3,
                            'taxable': true,
                            'type': null,
                            'unit_cost': 40,
                            'object': 'line_item',
                            'metadata': {},
                            'discounts': [],
                            'taxes': []
                        },
                        {
                            'amount': 46,
                            'catalog_item': null,
                            'created_at': 1572971873,
                            'description': '',
                            'discountable': true,
                            'id': 22828071,
                            'name': 'Supplies',
                            'quantity': 1,
                            'taxable': true,
                            'type': null,
                            'unit_cost': 46,
                            'object': 'line_item',
                            'metadata': {},
                            'discounts': [],
                            'taxes': []
                        }
                    ],
                    'metadata': {},
                    'name': 'Updated',
                    'needs_attention': false,
                    'next_chase_on': null,
                    'next_payment_attempt': null,
                    'notes': null,
                    'number': 'INV-0003',
                    'object': 'invoice',
                    'paid': false,
                    'payment_plan': null,
                    'payment_source': null,
                    'payment_terms': 'AutoPay',
                    'payment_url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi\/payment',
                    'pdf_url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi\/pdf',
                    'purchase_order': null,
                    'ship_to': {
                        'address1': '1234 Main St',
                        'address2': null,
                        'attention_to': null,
                        'city': 'Austin',
                        'country': 'US',
                        'created_at': 1574272634,
                        'name': 'Dr. Watson',
                        'postal_code': '78704',
                        'state': 'TX'
                    },
                    'shipping': [],
                    'status': 'past_due',
                    'subscription': null,
                    'subtotal': 166,
                    'taxes': [
                        {
                            'amount': 10,
                            'id': 2115754,
                            'object': 'tax',
                            'tax_rate': null
                        }
                    ],
                    'total': 174,
                    'url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi'
                }]";

            var mockHttp = new MockHttpMessageHandler();

            var filterByNameQ = new Dictionary<string, string> {{"filter[name]", "Abraham Lincoln"}};

            var filterByName = new Dictionary<string, object> {{"filter[name]", "Abraham Lincoln"}};

            var mockHeader = new Dictionary<string, string>();
            mockHeader["X-Total-Count"] = "1";
            mockHeader["Link"] =
                "<https://api.sandbox.invoiced.com/invoices?filter%5Bname%5D=Abraham+Lincoln&page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/invoices?filter%5Bname%5D=Abraham+Lincoln&page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/invoices?filter%5Bname%5D=Abraham+Lincoln&page=1>; rel=\"last\"";

            var request = mockHttp.When(HttpMethod.Get, "https://api.testmode.com/invoices").WithExactQueryString(filterByNameQ)
                .Respond(mockHeader, "application/json", jsonResponseListAll);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var invoice = conn.NewInvoice();

            var invoices = invoice.ListAll(filterByName);

            Assert.True(invoices[0].Id == 2334745);
        }

        [Fact]
        public void TestNewNote()
        {
            var jsonRequest = @"{
                'notes': 'example note'
            }";

            var jsonResponse = @"{
                'id': 1212,
                'customer': null,
                'notes': 'example note',
                'object': 'note'
            }";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://api.testmode.com/notes").WithJson(jsonRequest)
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var invoice = CreateDefaultInvoice(client);

            var testNote = invoice.NewNote();
            testNote.Notes = "example note";
            testNote.Create();

            Assert.True(testNote.Object == "note");
            Assert.True(testNote.Id == 1212);
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

            mockHttp.When(HttpMethod.Post, "https://api.testmode.com/invoices/2334745/letters")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var invoice = CreateDefaultInvoice(client);
            var response = invoice.SendLetter();

            Assert.True(response.State == "queued");
        }

        [Fact]
        public void TestPayInvoice()
        {
            const string jsonResponse = @"{
                    'attempt_count': 4,
                    'autopay': true,
                    'balance': 174,
                    'chase': false,
                    'closed': false,
                    'created_at': 1572971872,
                    'csv_url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi\/csv',
                    'currency': 'usd',
                    'customer': 576126,
                    'date': 1370062800,
                    'discounts': [
                        {
                            'amount': 2,
                            'expires': null,
                            'id': 2115753,
                            'object': 'discount',
                            'coupon': null
                        }
                    ],
                    'draft': false,
                    'due_date': null,
                    'id': 2334745,
                    'items': [
                        {
                            'amount': 120,
                            'catalog_item': null,
                            'created_at': 1572971873,
                            'description': '',
                            'discountable': true,
                            'id': 22828070,
                            'name': 'Doctor\'s Visit',
                            'quantity': 3,
                            'taxable': true,
                            'type': null,
                            'unit_cost': 40,
                            'object': 'line_item',
                            'metadata': {},
                            'discounts': [],
                            'taxes': []
                        },
                        {
                            'amount': 46,
                            'catalog_item': null,
                            'created_at': 1572971873,
                            'description': '',
                            'discountable': true,
                            'id': 22828071,
                            'name': 'Supplies',
                            'quantity': 1,
                            'taxable': true,
                            'type': null,
                            'unit_cost': 46,
                            'object': 'line_item',
                            'metadata': {},
                            'discounts': [],
                            'taxes': []
                        }
                    ],
                    'metadata': {},
                    'name': 'Updated',
                    'needs_attention': false,
                    'next_chase_on': null,
                    'next_payment_attempt': null,
                    'notes': null,
                    'number': 'INV-0003',
                    'object': 'invoice',
                    'paid': true,
                    'payment_plan': null,
                    'payment_source': null,
                    'payment_terms': 'AutoPay',
                    'payment_url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi\/payment',
                    'pdf_url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi\/pdf',
                    'purchase_order': null,
                    'ship_to': {
                        'address1': '1234 Main St',
                        'address2': null,
                        'attention_to': null,
                        'city': 'Austin',
                        'country': 'US',
                        'created_at': 1574272634,
                        'name': 'Dr. Watson',
                        'postal_code': '78704',
                        'state': 'TX'
                    },
                    'shipping': [],
                    'status': 'paid',
                    'subscription': null,
                    'subtotal': 166,
                    'taxes': [
                        {
                            'amount': 10,
                            'id': 2115754,
                            'object': 'tax',
                            'tax_rate': null
                        }
                    ],
                    'total': 174,
                    'url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi'
                }";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://api.testmode.com/invoices/2334745/pay")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var invoice = CreateDefaultInvoice(client);
            invoice.Pay();

            Assert.True(invoice.Id == 2334745);
            Assert.True(invoice.Customer == 576126);
            Assert.True(invoice.Paid);
        }

        [Fact]
        public void TestCreatePaymentPlan()
        {
            const string jsonResponse = @"{'approval':{'id':12,'ip':'192.168.1.1','timestamp':1234567893,
                'user_agent':'Mozilla\/5.0 (Macintosh; Intel Mac OS X 10.12; rv:50.0) Gecko\/20100101
                Firefox\/50.0'},'created_at':1234564892,
                'id':99,'installments':[{'amount':1000,'balance':1000,'date':1234567890,'id':23},
                {'amount':1000,'balance':1000,'date':1234567891,'id':24}],'object':'payment_plan',
                'status':'active'}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://api.testmode.com/invoices/2334745/payment_plan")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var invoice = CreateDefaultInvoice(client);
            var plan = invoice.NewPaymentPlan();
            plan.Create();

            Assert.True(plan.Approval != null);
            Assert.True(plan.Object == "payment_plan");
        }

        [Fact]
        public void TestRetrievePaymentPlan()
        {
            const string jsonResponse = @"{'approval':{'id':12,'ip':'192.168.1.1','timestamp':1234567893,
                'user_agent':'Mozilla\/5.0 (Macintosh; Intel Mac OS X 10.12; rv:50.0) Gecko\/20100101
                Firefox\/50.0'},'created_at':1234564892,
                'id':99,'installments':[{'amount':1000,'balance':1000,'date':1234567890,'id':23},
                {'amount':1000,'balance':1000,'date':1234567891,'id':24}],'object':'payment_plan',
                'status':'active'}";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Get, "https://api.testmode.com/invoices/2334745/payment_plan")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var invoice = CreateDefaultInvoice(client);
            var plan = invoice.NewPaymentPlan().Retrieve();

            Assert.True(plan.CreatedAt == 1234564892);
            Assert.True(plan.Object == "payment_plan");
        }

        [Fact]
        public void TestDeletePaymentPlan()
        {
            var mockHttp = new MockHttpMessageHandler();

            var request = mockHttp.When(HttpMethod.Delete, "https://api.testmode.com/invoices/2334745/payment_plan")
                .Respond(HttpStatusCode.NoContent);

            var client = mockHttp.ToHttpClient();

            var customer = CreateDefaultInvoice(client);
            var plan = customer.NewPaymentPlan();
            plan.Delete();
        }

        [Fact]
        public void TestListAttachments()
        {
            var jsonResponse = @"[{
	            'id': 13,
	            'file': {
		            'id': 13,
		            'object': 'file',
		            'name': 'logo-invoice.png',
		            'size': 6936,
		            'type': 'image/png',
		            'url': 'https://invoiced.com/img/logo-invoice.png',
		            'created_at': 1464625855
	            },
	            'created_at': 1464625855
            }]";

            var mockHttp = new MockHttpMessageHandler();

            var request = mockHttp.When(HttpMethod.Get, "https://api.testmode.com/invoices/2334745/attachments")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var invoice = CreateDefaultInvoice(client);

            var attachments = invoice.ListAttachments();

            Assert.True(attachments[0].Id == 13);
        }

        [Fact]
        public void TestVoid()
        {
            const string jsonResponse = @"{
                    'attempt_count': 4,
                    'autopay': true,
                    'balance': 174,
                    'chase': false,
                    'closed': false,
                    'created_at': 1572971872,
                    'csv_url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi\/csv',
                    'currency': 'usd',
                    'customer': 576126,
                    'date': 1370062800,
                    'discounts': [
                        {
                            'amount': 2,
                            'expires': null,
                            'id': 2115753,
                            'object': 'discount',
                            'coupon': null
                        }
                    ],
                    'draft': false,
                    'due_date': null,
                    'id': 2334745,
                    'items': [
                        {
                            'amount': 120,
                            'catalog_item': null,
                            'created_at': 1572971873,
                            'description': '',
                            'discountable': true,
                            'id': 22828070,
                            'name': 'Doctor\'s Visit',
                            'quantity': 3,
                            'taxable': true,
                            'type': null,
                            'unit_cost': 40,
                            'object': 'line_item',
                            'metadata': {},
                            'discounts': [],
                            'taxes': []
                        },
                        {
                            'amount': 46,
                            'catalog_item': null,
                            'created_at': 1572971873,
                            'description': '',
                            'discountable': true,
                            'id': 22828071,
                            'name': 'Supplies',
                            'quantity': 1,
                            'taxable': true,
                            'type': null,
                            'unit_cost': 46,
                            'object': 'line_item',
                            'metadata': {},
                            'discounts': [],
                            'taxes': []
                        }
                    ],
                    'metadata': {},
                    'name': 'Updated',
                    'needs_attention': false,
                    'next_chase_on': null,
                    'next_payment_attempt': null,
                    'notes': null,
                    'number': 'INV-0003',
                    'object': 'invoice',
                    'paid': false,
                    'payment_plan': null,
                    'payment_source': null,
                    'payment_terms': 'AutoPay',
                    'payment_url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi\/payment',
                    'pdf_url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi\/pdf',
                    'purchase_order': null,
                    'ship_to': {
                        'address1': '1234 Main St',
                        'address2': null,
                        'attention_to': null,
                        'city': 'Austin',
                        'country': 'US',
                        'created_at': 1574272634,
                        'name': 'Dr. Watson',
                        'postal_code': '78704',
                        'state': 'TX'
                    },
                    'shipping': [],
                    'status': 'voided',
                    'subscription': null,
                    'subtotal': 166,
                    'taxes': [
                        {
                            'amount': 10,
                            'id': 2115754,
                            'object': 'tax',
                            'tax_rate': null
                        }
                    ],
                    'total': 174,
                    'url': 'https:\/\/ajwt.sandbox.invoiced.com\/invoices\/hg2J8PtRIP70y2E3aPerARJi'
                }";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "https://api.testmode.com/invoices/2334745/void")
                .Respond("application/json", jsonResponse);

            var client = mockHttp.ToHttpClient();

            var conn = new Connection("voodoo", Environment.test);

            conn.TestClient(client);

            var invoice = CreateDefaultInvoice(client);
            invoice.Void();

            Assert.True(invoice.Id == 2334745);
            Assert.True(invoice.Customer == 576126);
            Assert.True(invoice.Status == "voided");
        }
    }
}