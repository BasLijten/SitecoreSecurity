using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Business.Models.ContentSecurityPolicy
{
    public class ManifestContentSecurityPolicySource : ContentSecurityPolicySource
    {
        private const string _name = "manifest-src";
        public ManifestContentSecurityPolicySource() : base(_name)
        { }
        public ManifestContentSecurityPolicySource(IContentSecurityPolicyOptions options, string hostnames) : base(_name, options, hostnames)
        { }
    }
}
