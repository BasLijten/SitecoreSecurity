using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Business.Models.ContentSecurityPolicy
{
    public class ChildContentSecurityPolicySource : ContentSecurityPolicySource
    {
        private const string _name = "child-src";
        public ChildContentSecurityPolicySource() : base(_name)
        { }
        public ChildContentSecurityPolicySource(IContentSecurityPolicyOptions options, string hostnames) : base(_name, options, hostnames)
        { }
    }
}
