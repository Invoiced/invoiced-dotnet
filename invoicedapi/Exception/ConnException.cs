namespace Invoiced
{
    public class ConnException : InvoicedException
    {
        private const long serialVersionUID = 1L;

        public ConnException(string message) : base(message)
        {
        }
    }
}