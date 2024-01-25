using Marketplace.Framework;
using System;

namespace Marketplace.Domain
{

    public class ClassifiedAdId : Value<ClassifiedAdId>
    {
        private readonly Guid _value;

        public ClassifiedAdId(Guid value) => _value = value;    
        
    }
}
