using System.Runtime.Serialization;

namespace WebShop.Business.Exceptions
{
    [Serializable]
    public class BasketNotFoundException : Exception
    {
        public BasketNotFoundException()
        {
        }

        public BasketNotFoundException(string message) : base(message)
        {
        }

        public BasketNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BasketNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}