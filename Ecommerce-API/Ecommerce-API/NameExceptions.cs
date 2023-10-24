namespace Ecommerce_API
{
    public class NameExceptions : Exception
    {
        public NameExceptions()
        {

        }

        public NameExceptions(string message) : base(message)
        {

        }

        public NameExceptions(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
