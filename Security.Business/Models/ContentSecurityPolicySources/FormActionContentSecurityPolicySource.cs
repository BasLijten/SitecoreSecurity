using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Business.Models.ContentSecurityPolicy
{
    public class FormActionContentSecurityPolicySource : ContentSecurityPolicySource
    {
        private const string _name = "form-action";
        public FormActionContentSecurityPolicySource() : base(_name)
        { }
        public FormActionContentSecurityPolicySource(IContentSecurityPolicyOptions options, string hostnames) : base(_name, options, hostnames)
        { }
    }
}
