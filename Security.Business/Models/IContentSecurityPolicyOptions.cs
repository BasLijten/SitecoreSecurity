using System.Collections.Generic;

namespace Security.Business.Models
{
    public interface IContentSecurityPolicyOptions
    {
        bool None { get; set; }
        bool All { get; set; }
        bool Self { get; set; }
        bool Data { get; set; }
        bool UnsafeInline { get; set; }
        bool UnsafeEval { get; set; }        
    }
}