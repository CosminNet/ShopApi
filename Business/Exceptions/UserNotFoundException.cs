using System.Runtime.Serialization;

namespace WebShop.Business.Exceptions
{
    [Serializable]
    public class UserNotFoundException : Exception
    {
        public string Name { get; private set; }
     
        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string name, string message = "") : base(message)
        {
            Name = name;
        }        
    }
}