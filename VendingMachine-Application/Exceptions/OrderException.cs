using System;
using System.Runtime.Serialization;

namespace VendingMachine_Application
{
    [Serializable]
    internal class OrderException : Exception
    {
        public OrderException()
        {
        }

        public OrderException(string message, string message1) : base(message)
        {
        }

        public OrderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}