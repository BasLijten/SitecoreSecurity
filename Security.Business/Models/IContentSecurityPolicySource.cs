using System.Collections;
using System.Collections.Generic;

namespace Security.Business.Models
{
    public interface IContentSecurityPolicySource
    {
        string Name { get; }
        IContentSecurityPolicyOptions Options { get; set; }
        string Hostnames {get;set;}
    }
}