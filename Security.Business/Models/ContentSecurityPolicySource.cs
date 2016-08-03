using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Business.Models
{
    public class ContentSecurityPolicySource : IContentSecurityPolicySource
    {
        private string _name;
        private IContentSecurityPolicyOptions _options;
        public ContentSecurityPolicySource()
        {

        }
        public ContentSecurityPolicySource(string name)
        {
            this._name = name;
        }
        public ContentSecurityPolicySource(IContentSecurityPolicySource source)
        {
            if (source != null)
            {
                this.Options = source.Options;
                this.Hostnames = source.Hostnames;
            }
        }
        public ContentSecurityPolicySource(IContentSecurityPolicyOptions options, string hostnames)
        {            
            this.Hostnames = hostnames;
            this.Options = options;
        }
        public ContentSecurityPolicySource(string name, IContentSecurityPolicyOptions options, string hostnames)
        {
            this._name = name;
            this.Hostnames = hostnames;
            this.Options = options;
        }
        public string Name { get { return this._name; } }
        public string Hostnames { get; set; }

        public IContentSecurityPolicyOptions Options
        {
            get
            {
                if (_options == null)
                    _options = new ContentSecurityPolicyOptions();
                return _options;             
            }
            set
            {
                _options = value;
            }

        }

    }
}
