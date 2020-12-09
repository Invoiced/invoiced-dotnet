using System;
using Newtonsoft.Json;

namespace Invoiced
{
    public class InvoicedException : Exception
    {
        public InvoicedException(string exceptionMessage) : base(exceptionMessage)
        {
            JsonConvert.DeserializeObject<Error>(exceptionMessage);
        }

        public Error InvoicedError { get; }
    }
}