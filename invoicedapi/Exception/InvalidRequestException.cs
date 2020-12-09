namespace Invoiced
{
    public class InvalidRequestException : InvoicedException
    {
        private const long serialVersionUID = 1L;

        public InvalidRequestException(string message) : base(message)
        {
        }
    }
}