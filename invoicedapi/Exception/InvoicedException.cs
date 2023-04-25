using System;
using Newtonsoft.Json;

namespace Invoiced
{
    public class InvoicedException : Exception
    {
        public InvoicedException(string exceptionMessage) : base(exceptionMessage)
        {
        }

        public Error InvoicedError { get; }
    }
}