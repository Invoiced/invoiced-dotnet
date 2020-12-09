namespace Invoiced
{
    public class InternalServerException : InvoicedException
    {
        private const long serialVersionUID = 1L;

        public InternalServerException(string message) : base(message)
        {
        }
    }
}