using System;
using Xunit;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Invoiced;
using System.Net;

namespace InvoicedTest
{
    public class InvoiceTest: BaseTest
    {
        [Fact]
        public void Deserialize()
        {
            var invoiceJson = @"{
                'id': 46225,
                'object': 'invoice',
                'customer': '15444',
                'name': 'null',
                'currency': 'usd',
                'draft': false,
                'closed': false,
                'paid': false,
                'status': 'not-sent',
                'autopay': 'false',
                'attempt_count': 0,
                'next_payment_attempt': null,
                'subscription': null,
                'number': 'INV-0016',
                'date': 1416290400,
                'due_date': 1417500000,
                'payment_terms': 'NET 14',
                'items': [
                    {
                        'id': 7,
                        'object': 'line_item',
                        'catalog_item': null,
                        'type': 'product',
                        'name': 'Copy Paper, Case',
                        'description': null,
                        'quantity': 1,
                        'unit_cost': 45,
                        'amount': 45,
                        'discountable': true,
                        'discounts': [],
                        'taxable': true,
                        'taxes': [],
                        'metadata': {}
                    },
                    {
                        'id': 8,
                        'object': 'line_item',
                        'catalog_item': 'delivery',
                        'type': 'service',
                        'name': 'Delivery',
                        'description': null,
                        'quantity': 1,
                        'unit_cost': 10,
                        'amount': 10,
                        'discountable': true,
                        'discounts': [],
                        'taxable': true,
                        'taxes': [],
                        'metadata': {}
                    },
                ],
                'notes': null,
                'subtotal': 55,
                'discounts': [],
                'taxes': [
                    {
                        'id': 20554,
                        'object': 'tax',
                        'amount': 3.85,
                        'tax_rate': null
                    }
                ],
                'total': 51.15,
                'balance': 51.15,
                'url': 'https://dundermifflin.invoiced.com/invoices/IZmXbVOPyvfD3GPBmyd6FwXY',
                'payment_url': 'https://dundermifflin.invoiced.com/invoices/IZmXbVOPyvfD3GPBmyd6FwXY/payment',
                'pdf_url': 'https://dundermifflin.invoiced.com/invoices/IZmXbVOPyvfD3GPBmyd6FwXY/pdf',
                'created_at': 1415229884,
                'metadata': {}
            }";
            var invoice = JsonConvert.DeserializeObject<Invoice>(invoiceJson);

            Assert.True(invoice.Customer == 15444);

            Assert.Equal(invoice.Items[0].Id, 7);
            Assert.Equal(invoice.Items[0].Type, "product");

            Assert.Equal(8, invoice.Items[1].Id);
            Assert.Equal("service", invoice.Items[1].Type);
        }

        [Fact]
        public void Retrieve()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp
                .When(HttpMethod.Get ,"https://testmode/invoices/46225")
                .Respond("application/json", "{'id' : 46225, 'object' : 'invoice'}");

            var conn = this.CreateTestConnection(mockHttp.ToHttpClient());

            var invoiceConnection = conn.NewInvoice();

            var invoice = invoiceConnection.Retrieve(46225);

            Assert.Equal(46225, invoice.Id);
            Assert.Equal("invoice", invoice.Object);
        }

        [Fact]
        public void Create()
        {
            var invoiceCreateResponse = @"{
                'id': 46225,
                'object': 'invoice',
                'customer': 15444,
                'name': 'null',
                'currency': 'usd',
                'draft': false,
                'closed': false,
                'paid': false,
                'status': 'not-sent',
                'autopay': 'false',
                'attempt_count': 0,
                'next_payment_attempt': null,
                'subscription': null,
                'number': 'INV-0016',
                'date': 1416290400,
                'due_date': 1417500000,
                'payment_terms': 'NET 14',
                'items': [
                    {
                        'id': 7,
                        'object': 'line_item',
                        'catalog_item': null,
                        'type': 'product',
                        'name': 'Copy Paper, Case',
                        'description': null,
                        'quantity': 1,
                        'unit_cost': 45,
                        'amount': 45,
                        'discountable': true,
                        'discounts': [],
                        'taxable': true,
                        'taxes': [],
                        'metadata': {}
                    },
                    {
                        'id': 8,
                        'object': 'line_item',
                        'catalog_item': 'delivery',
                        'type': 'service',
                        'name': 'Delivery',
                        'description': null,
                        'quantity': 1,
                        'unit_cost': 10,
                        'amount': 10,
                        'discountable': true,
                        'discounts': [],
                        'taxable': true,
                        'taxes': [],
                        'metadata': {}
                    },
                ],
                'notes': null,
                'subtotal': 55,
                'discounts': [],
                'taxes': [
                    {
                        'id': 20554,
                        'object': 'tax',
                        'amount': 3.85,
                        'tax_rate': null
                    }
                ],
                'total': 51.15,
                'balance': 51.15,
                'url': 'https://dundermifflin.invoiced.com/invoices/IZmXbVOPyvfD3GPBmyd6FwXY',
                'payment_url': 'https://dundermifflin.invoiced.com/invoices/IZmXbVOPyvfD3GPBmyd6FwXY/payment',
                'pdf_url': 'https://dundermifflin.invoiced.com/invoices/IZmXbVOPyvfD3GPBmyd6FwXY/pdf',
                'created_at': 1415229884,
                'metadata': {}
            }";


            var invoiceCreateRequest = @"{
                'customer': 15444,
                'payment_terms': 'NET 14',
                'items': [
                    {
                        'name': 'Copy paper, Case',
                        'quantity': 1,
                        'unit_cost': 45.0
                    },
                    {
                        'catalog_item': 'delivery',
                        'quantity': 1
                    }
                ],
                'taxes': [
                    {
                        'amount': 3.85
                    }
                ]
            }";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp
                .When(HttpMethod.Post, "https://testmode/invoices")
                .WithJson(invoiceCreateRequest)
                .Respond("application/json", invoiceCreateResponse);

            var connection = this.CreateTestConnection(mockHttp.ToHttpClient());

            var invoice = connection.NewInvoice();

            invoice.Customer = 15444;
            invoice.PaymentTerms = "NET 14";
            invoice.Items = new List<LineItem>(){
                new LineItem() {
                    Name = "Copy paper, Case",
                    Quantity = 1,
                    UnitCost = 45
                },
                new LineItem() {
                    CatalogItem = "delivery",
                    Quantity = 1
                }
            };
            invoice.Taxes = (new List<Tax>() { 
                new Tax() { Amount = 3.85 }
            });
            invoice.Create();

            Assert.Equal(15444, invoice.Customer);
            Assert.Equal("NET 14", invoice.PaymentTerms);

            Assert.Equal("Copy paper, Case", invoice.Items[0].Name);
            Assert.Equal(1, invoice.Items[0].Quantity);
            Assert.Equal(45, invoice.Items[0].UnitCost);

            Assert.Equal("delivery", invoice.Items[1].CatalogItem);
            Assert.Equal(1, invoice.Items[1].Quantity);
            Assert.Equal(3.85, invoice.Taxes[0].Amount);
        }

        [Fact]
        public void Save()
        {
            var invoiceSaveResponse = @"{
                'id': 1233,
                'name': 'July Paper Delivery',
                'notes': 'The order was delivered on Jul 20, 2015',
            }";

            var invoiceSaveRequest = @"{
                'name': 'July Paper Delivery',
                'notes': 'The order was delivered on Jul 20, 2015',
            }";

            var mockHttp = new MockHttpMessageHandler();
            var request = mockHttp
                .When(new HttpMethod("PATCH"), "https://testmode/invoices/1233")
                .WithJson(invoiceSaveRequest).Respond("application/json", invoiceSaveResponse);

            var client = mockHttp.ToHttpClient();

            var json = @"{'id': 46225}";
            var invoice = JsonConvert.DeserializeObject<Invoice>(json);

            var connection = this.CreateTestConnection(mockHttp.ToHttpClient());

            invoice.ChangeConnection(connection);

            invoice.Id = 1233;
            invoice.Name = "July Paper Delivery";
            invoice.Notes = "The order was delivered on Jul 20, 2015";
            invoice.SaveAll();

            Assert.Equal(1233, invoice.Id);
            Assert.Equal("July Paper Delivery", invoice.Name);
            Assert.Equal("The order was delivered on Jul 20, 2015", invoice.Notes);
        }

        [Fact]
        public void Delete()
        {

            var mockHttp = new MockHttpMessageHandler();

            var request = mockHttp
                .When(HttpMethod.Delete, "https://testmode/invoices/46225")
                .Respond(HttpStatusCode.NoContent);
                

            var json = @"{'id': 46225}";
            var invoice = JsonConvert.DeserializeObject<Invoice>(json);

            var connection = this.CreateTestConnection(mockHttp.ToHttpClient());

            invoice.ChangeConnection(connection);

            invoice.Delete();
            Assert.Equal(1, mockHttp.GetMatchCount(request));
        }

        [Fact]
        public void ListAll()
        {
            var invoiceListAllResponse = @"[
                {
                    'id': 46225,
                    'object': 'invoice',
                    'customer': '15444',
                    'name': 'null',
                    'currency': 'usd',
                    'draft': false,
                    'closed': false,
                    'paid': false,
                    'status': 'not-sent',
                    'autopay': 'false',
                    'attempt_count': 0,
                    'next_payment_attempt': null,
                    'subscription': null,
                    'number': 'INV-0016',
                    'date': 1416290400,
                    'due_date': 1417500000,
                    'payment_terms': 'NET 14',
                    'items': [
                        {
                            'id': 7,
                            'object': 'line_item',
                            'catalog_item': null,
                            'type': 'product',
                            'name': 'Copy Paper, Case',
                            'description': null,
                            'quantity': 1,
                            'unit_cost': 45,
                            'amount': 45,
                            'discountable': true,
                            'discounts': [],
                            'taxable': true,
                            'taxes': [],
                            'metadata': {}
                        },
                        {
                            'id': 8,
                            'object': 'line_item',
                            'catalog_item': 'delivery',
                            'type': 'service',
                            'name': 'Delivery',
                            'description': null,
                            'quantity': 1,
                            'unit_cost': 10,
                            'amount': 10,
                            'discountable': true,
                            'discounts': [],
                            'taxable': true,
                            'taxes': [],
                            'metadata': {}
                        },
                    ],
                    'notes': null,
                    'subtotal': 55,
                    'discounts': [],
                    'taxes': [
                        {
                            'id': 20554,
                            'object': 'tax',
                            'amount': 3.85,
                            'tax_rate': null
                        }
                    ],
                    'total': 51.15,
                    'balance': 51.15,
                    'url': 'https://dundermifflin.invoiced.com/invoices/IZmXbVOPyvfD3GPBmyd6FwXY',
                    'payment_url': 'https://dundermifflin.invoiced.com/invoices/IZmXbVOPyvfD3GPBmyd6FwXY/payment',
                    'pdf_url': 'https://dundermifflin.invoiced.com/invoices/IZmXbVOPyvfD3GPBmyd6FwXY/pdf',
                    'created_at': 1415229884,
                    'metadata': {}
                }
            ]";

            var mockHttp = new MockHttpMessageHandler();

            var filterByNameQ = new Dictionary<string, string> { { "filter[name]", "Abraham Lincoln" } };

            var filterByName = new Dictionary<string, Object> { { "filter[name]", "Abraham Lincoln" } };

            var mockHeader = new Dictionary<string, string>();
            mockHeader["X-Total-Count"] = "1";
            mockHeader["Link"] = "<https://api.sandbox.invoiced.com/customers?filter%5Bname%5D=Abraham+Lincoln&page=1>; rel=\"self\", <https://api.sandbox.invoiced.com/invoices?filter%5Bname%5D=Abraham+Lincoln&page=1>; rel=\"first\", <https://api.sandbox.invoiced.com/customers?filter%5Bname%5D=Abraham+Lincoln&page=1>; rel=\"last\"";

            var request = mockHttp.When(HttpMethod.Get, "https://testmode/invoices").WithExactQueryString(filterByNameQ).Respond(mockHeader, "application/json", invoiceListAllResponse);

            var connection = this.CreateTestConnection(mockHttp.ToHttpClient());

            var invoice = connection.NewInvoice();

            var invoices = invoice.ListAll(filterByName);

            Assert.True(invoices[0].Id == 46225);
        }
    }
}