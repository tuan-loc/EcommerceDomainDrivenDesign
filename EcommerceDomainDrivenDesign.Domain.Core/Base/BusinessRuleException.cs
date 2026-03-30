using System;

namespace EcommerceDomainDrivenDesign.Domain.Core.Base
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string message) : base(message) { }
    }
}
