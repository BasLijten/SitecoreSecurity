using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Business.Models
{
    public class ContentSecurityPolicyOptions : IContentSecurityPolicyOptions
    {
        public bool All { get; set; }

        public bool Data { get; set; }

        public bool None { get; set; }

        public bool Self { get; set; }

        public bool UnsafeEval { get; set; }
        public bool UnsafeInline { get; set; }
    }

}
