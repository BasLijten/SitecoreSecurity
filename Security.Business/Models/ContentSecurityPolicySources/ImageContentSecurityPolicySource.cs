using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Business.Models.ContentSecurityPolicy
{
    public class ImageContentSecurityPolicySource : ContentSecurityPolicySource
    {
        private const string _name = "img-src";
        public ImageContentSecurityPolicySource() : base(_name)
        { }
        public ImageContentSecurityPolicySource(IContentSecurityPolicyOptions options, string hostnames) : base(_name, options, hostnames)
        { }
    }
}
