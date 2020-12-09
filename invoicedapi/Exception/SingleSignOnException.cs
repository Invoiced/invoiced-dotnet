namespace Invoiced
{
    public class SingleSignOnException : InvoicedException
    {
        private const long serialVersionUID = 1L;

        public SingleSignOnException(string message) : base(message)
        {
        }
    }
}