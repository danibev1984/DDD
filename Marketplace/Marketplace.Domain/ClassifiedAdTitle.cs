using Marketplace.Framework;
using System;

namespace Marketplace.Domain
{
    public class ClassifiedAdTitle : Value<ClassifiedAdTitle>
    {
        public static ClassifiedAdTitle FromString(string title)
        {
            CheckValidity(title);
            return new ClassifiedAdTitle(title);
        }

        private readonly string _value;
        
        internal ClassifiedAdTitle(string value) => _value = value; 

        private static void CheckValidity(string title)
        {
            if(title.Length > 100)
                throw new ArgumentOutOfRangeException("Title cannot be longer than 100 characters", nameof(title));
        }

        public static implicit operator string(ClassifiedAdTitle self) => self._value;
    }
}
