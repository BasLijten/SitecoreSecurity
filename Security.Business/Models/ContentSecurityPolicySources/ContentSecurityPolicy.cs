using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Business.Models.ContentSecurityPolicySources
{
    public class ContentSecurityPolicy : IContentSecurityPolicy
    {
        public IContentSecurityPolicySource Default
        { get;set; }

        public IContentSecurityPolicySource Script
        { get; set; }

        public IContentSecurityPolicySource Style
        { get; set; }

        public IContentSecurityPolicySource Image
        { get; set; }

        public IContentSecurityPolicySource Font
        { get; set; }

        public IContentSecurityPolicySource Connect
        { get; set; }

        public IContentSecurityPolicySource Media
        { get; set; }

        public IContentSecurityPolicySource Object
        { get; set; }

        public IContentSecurityPolicySource Child
        { get; set; }

        public IContentSecurityPolicySource FrameAncestors
        { get; set; }

        public IContentSecurityPolicySource FormAction
        { get; set; }

        public IContentSecurityPolicySource Manifest
        { get; set; }

        public bool UpgradeInsecureRequests
        { get; set; }

        public bool BlockAllMixedContent
        { get; set; }

        public string BaseUri
        { get; set; }

        public string PluginTypes
        { get; set; }

        public bool ReportOnly
        { get; set; }

        public string ReportUri
        { get; set; }

        public IContentSecurityPolicySandbox Sandbox { get; set; }

        public IContentSecurityPolicyReflectedXss ReflectedXss { get; set; }

        public IContentSecurityPolicyReferrer Referrer { get; set; }

        //private IContentSecurityPolicySource _default = null;
        //private IContentSecurityPolicySource _script = null;
        //private IContentSecurityPolicySource _style = null;
        //private IContentSecurityPolicySource _image = null;
        //private IContentSecurityPolicySource _font = null;
        //private IContentSecurityPolicySource _connect = null;
        //private IContentSecurityPolicySource _media = null;
        //private IContentSecurityPolicySource _object = null;
        //private IContentSecurityPolicySource _child = null;
        //private IContentSecurityPolicySource _frameAncestor = null;
        //private IContentSecurityPolicySource _formAction = null;        

        //public IContentSecurityPolicySource Default {
        //    get { return _default ?? new ContentSecurityPolicySource("default-src"); }               
        //}

        //public IContentSecurityPolicySource Script {
        //    get { return _script ?? new ContentSecurityPolicySource("script-src"); }            
        //}

        //public IContentSecurityPolicySource Style
        //{
        //    get { return _style ?? new ContentSecurityPolicySource("style-src"); }            
        //}

        //public IContentSecurityPolicySource Image
        //{
        //    get { return _image ?? new ContentSecurityPolicySource("img-src"); }
        //}

        //public IContentSecurityPolicySource Font
        //{
        //    get { return _font ?? new ContentSecurityPolicySource("font-src"); }
        //}

        //public IContentSecurityPolicySource Connect
        //{
        //    get { return _connect ?? new ContentSecurityPolicySource("connect-src"); }
        //}

        //public IContentSecurityPolicySource Media
        //{
        //    get { return _media ?? new ContentSecurityPolicySource("media-src"); }
        //}

        //public IContentSecurityPolicySource Object
        //{
        //    get { return _object ?? new ContentSecurityPolicySource("object-src"); }
        //}

        //public IContentSecurityPolicySource Child
        //{
        //    get { return _child ?? new ContentSecurityPolicySource("child-src"); }
        //}

        //public IContentSecurityPolicySource FrameAncestors
        //{
        //    get { return _object ?? new ContentSecurityPolicySource("frame-ancestors"); }
        //}

        //public IContentSecurityPolicySource FormAction
        //{
        //    get { return _object ?? new ContentSecurityPolicySource("form-action"); }
        //}

        public override string ToString()
        {
            var cspCreater = new ContentSecurityPolicyCreater(this);
            var result = cspCreater.Create();
            return result;
        }
    }
        
    
    

    

}
