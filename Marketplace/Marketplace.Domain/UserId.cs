using Marketplace.Framework;
using System;

namespace Marketplace.Domain
{
    public class UserId : Value<UserId>
    {
        private readonly Guid _value;

        public UserId(Guid value) => _value = value;
    }
}
