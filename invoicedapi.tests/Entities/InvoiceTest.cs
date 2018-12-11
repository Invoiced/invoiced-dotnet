using System;
using Xunit;
using Invoiced;
using System.Net.Http;
using System.Collections.Generic;


namespace InvoicedTest
{
    public class InvoiceTest
    {
        [Fact]
        public void serialize()
        {
            var invoice = new Invoice(null);
            invoice.id = 3;
            // Console.WriteLine(invoice.ToString());
                 
        }

        

    }
}
