using System.Runtime.Serialization;

namespace WebShop.Business.Exceptions
{
    [Serializable]
    public class BasketClosedException : Exception
    {
        public BasketClosedException()
        {
        }

        public BasketClosedException(string message) : base(message)
        {
        }

        public BasketClosedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BasketClosedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}