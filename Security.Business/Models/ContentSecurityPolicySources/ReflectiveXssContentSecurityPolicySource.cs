using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Business.Models.ContentSecurityPolicySources
{
    public class ReflectiveXssContentSecurityPolicySource : IContentSecurityPolicyReflectedXss
    {
        string _name = "reflected-xss";
        public string Name { get { return _name; } }
        public string Mode { get; set; } = "block";
    }
}
