using System;
using System.Runtime.Serialization;

namespace Marketplace.Domain
{
    [Serializable]
    public class InvalidEntityStateException : Exception
    {
        private ClassifiedAd classifiedAd;
        private string v;

        public InvalidEntityStateException()
        {
        }

        public InvalidEntityStateException(string message) : base(message)
        {
        }

        public InvalidEntityStateException(ClassifiedAd classifiedAd, string v)
        {
            this.classifiedAd = classifiedAd;
            this.v = v;
        }

        public InvalidEntityStateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidEntityStateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}