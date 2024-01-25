using Marketplace.Framework;
using System;

namespace Marketplace.Domain
{
    public class ClassifiedAdTitle : Value<ClassifiedAdTitle>
    {
        public static ClassifiedAdTitle FromString(string title) => new ClassifiedAdTitle(title);

        private readonly string _value;
        private ClassifiedAdTitle(string value)
        {
            if(value.Length > 100)
                throw new ArgumentOutOfRangeException("Title cannot be longer than 100 characters", nameof(value));

            _value = value;
        }
    }
}
