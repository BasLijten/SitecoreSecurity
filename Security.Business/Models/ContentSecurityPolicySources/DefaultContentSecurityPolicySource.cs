using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Business.Models
{
    public class DefaultContentSecurityPolicySource: ContentSecurityPolicySource
    {
        private const string _name = "default-src";
        public DefaultContentSecurityPolicySource() : base(_name)
        { }
        public DefaultContentSecurityPolicySource(IContentSecurityPolicyOptions options, string hostnames): base(_name, options, hostnames)
        { }        
    }
}
