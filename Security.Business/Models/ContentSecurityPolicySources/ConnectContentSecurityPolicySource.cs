using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Business.Models.ContentSecurityPolicy
{
    public class ConnectContentSecurityPolicySource : ContentSecurityPolicySource
    {
        private const string _name = "connect-src";
        public ConnectContentSecurityPolicySource() : base(_name)
        { }
        public ConnectContentSecurityPolicySource(IContentSecurityPolicyOptions options, string hostnames) : base(_name, options, hostnames)
        { }
    }
}
