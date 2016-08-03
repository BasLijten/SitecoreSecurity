using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Business.Models.ContentSecurityPolicy
{
    public class FontContentSecurityPolicySource : ContentSecurityPolicySource
    {
        private const string _name = "font-src";
        public FontContentSecurityPolicySource() : base(_name)
        { }
        public FontContentSecurityPolicySource(IContentSecurityPolicyOptions options, string hostnames) : base(_name, options, hostnames)
        { }
    }
}
