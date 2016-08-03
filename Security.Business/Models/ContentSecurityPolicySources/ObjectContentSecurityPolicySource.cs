using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Business.Models.ContentSecurityPolicy
{
    public class ObjectContentSecurityPolicySource : ContentSecurityPolicySource
    {
        private const string _name = "object-src";
        public ObjectContentSecurityPolicySource() : base(_name)
        { }
        public ObjectContentSecurityPolicySource(IContentSecurityPolicyOptions options, string hostnames) : base(_name, options, hostnames)
        { }
    }
}
