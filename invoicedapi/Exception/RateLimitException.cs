namespace Invoiced
{
    public class RateLimitException : InvoicedException
    {
        private const long serialVersionUID = 1L;

        public RateLimitException(string message) : base(message)
        {
        }
    }
}