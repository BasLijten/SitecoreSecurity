using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Business.Models
{
    public class StyleContentSecurityPolicySource: ContentSecurityPolicySource
    {
        private const string _name = "style-src";
        public StyleContentSecurityPolicySource() : base(_name)
        { }
        public StyleContentSecurityPolicySource(IContentSecurityPolicyOptions options, string hostnames): base(_name, options, hostnames)
        { }        
    }    

    

    

    

    

    

    

    

    
}
