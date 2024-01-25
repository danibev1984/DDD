using Marketplace.Framework;
using System;

namespace Marketplace.Domain
{
    public class ClassifiedAdText : Value<ClassifiedAdText>
    {
        public static ClassifiedAdText FromString(string text) => new ClassifiedAdText(text);

        private readonly string _value;
        private ClassifiedAdText(string value)
        {
            if (value.Length > 100)
                throw new ArgumentOutOfRangeException("Text cannot be longer than 100 characters", nameof(value));

            _value = value;
        }
    }
}
