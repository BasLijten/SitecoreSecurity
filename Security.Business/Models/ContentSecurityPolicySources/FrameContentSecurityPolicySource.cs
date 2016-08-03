using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Business.Models.ContentSecurityPolicy
{
    public class FrameAncestorContentSecurityPolicySource : ContentSecurityPolicySource
    {
        private const string _name = "frame-ancestors";
        public FrameAncestorContentSecurityPolicySource() : base(_name)
        { }
        public FrameAncestorContentSecurityPolicySource(IContentSecurityPolicyOptions options, string hostnames) : base(_name, options, hostnames)
        { }
    }
}
