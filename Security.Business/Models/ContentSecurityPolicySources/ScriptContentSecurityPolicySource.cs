using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Business.Models
{
    public class ScriptContentSecurityPolicySource: ContentSecurityPolicySource
    {
        private const string _name = "script-src";
        public ScriptContentSecurityPolicySource() : base(_name)
        { }
        public ScriptContentSecurityPolicySource(IContentSecurityPolicyOptions options, string hostnames): base(_name, options, hostnames)
        { }


        
    }
}
