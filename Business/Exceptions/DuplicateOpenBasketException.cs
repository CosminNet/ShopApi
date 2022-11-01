using System.Runtime.Serialization;

namespace WebShop.Business.Exceptions
{
    [Serializable]
    public class DuplicateOpenBasketException : Exception
    {
        public DuplicateOpenBasketException()
        {
        }

        public DuplicateOpenBasketException(string message) : base(message)
        {
        }

        public DuplicateOpenBasketException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateOpenBasketException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}