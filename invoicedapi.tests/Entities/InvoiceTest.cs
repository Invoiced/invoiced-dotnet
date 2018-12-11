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
        public void Serialize()
        {
            var invoice = new Invoice(null);
             Console.WriteLine(invoice.ToString());
                 
        }

        

    }
}
