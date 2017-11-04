
namespace Invoiced
{
    using System;
    using System.Net;
    using Newtonsoft.Json;

    public class InvoicedException : Exception {
        public Error InvoicedError { get;}

        public InvoicedException(string exceptionMessage) : base(exceptionMessage) {

                JsonConvert.DeserializeObject<Error>(exceptionMessage);  


    }
    }


}