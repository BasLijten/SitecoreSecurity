using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Business.Models.ContentSecurityPolicy
{
    public class MediaContentSecurityPolicySource : ContentSecurityPolicySource
    {
        private const string _name = "media-src";
        public MediaContentSecurityPolicySource() : base(_name)
        { }
        public MediaContentSecurityPolicySource(IContentSecurityPolicyOptions options, string hostnames) : base(_name, options, hostnames)
        { }
    }
}
