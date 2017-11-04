
namespace Invoiced
{
    using System;
    using System.Net;

    public class InvalidRequestException : InvoicedException {       
        
         private const long serialVersionUID = 1L;

         public InvalidRequestException(string message) : base(message) {
    
         }

    }

}