using System;
using System.Runtime.Serialization;

namespace Marketplace.Domain
{
    [Serializable]
    internal class CurrencyMismatchException : Exception
    {
        public CurrencyMismatchException()
        {
        }

        public CurrencyMismatchException(string message) : base(message)
        {
        }

        public CurrencyMismatchException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CurrencyMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}