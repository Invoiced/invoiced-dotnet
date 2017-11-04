namespace Invoiced
{
    public class AuthException : InvoicedException {       
        
         private const long serialVersionUID = 1L;

         public AuthException(string message) : base(message) {
    
         }

    }

}