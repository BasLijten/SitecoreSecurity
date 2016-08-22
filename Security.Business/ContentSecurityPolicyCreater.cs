using Security.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Business
{
    class ContentSecurityPolicyCreater
    {
        private const string None = "'none' ";
        private const string All = "* ";
        private const string Data = "data: ";
        private const string Self = "'self' ";
        private const string UnsafeInline = "'unsafe-inline' ";
        private const string UnsafeEval = "'unsafe-eval' ";

        IContentSecurityPolicy _policy { get; }
        public ContentSecurityPolicyCreater(IContentSecurityPolicy policy)
        {
            this._policy = policy;
        }

        private string GeneratePolicyOptions(IContentSecurityPolicyOptions options)
        {
            string result = String.Empty;
            if (options.None)
            {
                result = None;
            }
            else
            {
                if (options.All) result += All;
                if (options.Data) result += Data;
                if (options.Self) result += Self;
                if (options.UnsafeInline) result += UnsafeInline;
                if (options.UnsafeEval) result += UnsafeEval;
            }
            return result;
        }        

        private string GenerateSourcePolicy(IContentSecurityPolicySource csp)
        {
            string source = String.Empty;
            if (csp != null)
            {
                var policyOptions = GeneratePolicyOptions(csp.Options);
                if (String.IsNullOrEmpty(policyOptions.Trim()) && String.IsNullOrEmpty(csp.Hostnames.Trim()))
                    return source;

                source += String.Concat(csp.Name, " ");                 
                source += GeneratePolicyOptions(csp.Options);

                if (!csp.Options.None)
                    source += csp.Hostnames;
                
                if (String.IsNullOrEmpty(source))
                    return String.Empty;
                source += ";";
            }
            return source;
        }

        private string CreatePolicy()
        {
            string policy = String.Empty;
            policy = GenerateSourcePolicy(this._policy.Default);
            policy += GenerateSourcePolicy(this._policy.Script);
            policy += GenerateSourcePolicy(this._policy.Style);
            policy += GenerateSourcePolicy(this._policy.Image);
            policy += GenerateSourcePolicy(this._policy.Font);
            policy += GenerateSourcePolicy(this._policy.Connect);
            policy += GenerateSourcePolicy(this._policy.Media);
            policy += GenerateSourcePolicy(this._policy.Object);
            policy += GenerateSourcePolicy(this._policy.Child);
            policy += GenerateSourcePolicy(this._policy.FrameAncestors);
            policy += GenerateSourcePolicy(this._policy.FormAction);
            policy += GenerateSourcePolicy(this._policy.Manifest);
            policy += GenerateReflectiveXssPolicy(this._policy.ReflectedXss);
            policy += GenerateUpgradeInsecureRequests(this._policy.UpgradeInsecureRequests);
            policy += GenerateBlockAllMixedContent(_policy.BlockAllMixedContent);
            policy += GenerateBaseUri(_policy.BaseUri);
            policy += GeneratePluginTypes(_policy.PluginTypes);
            policy += GenerateReportUri(_policy.ReportUri);
            return policy;
        }

        private string GenerateReflectiveXssPolicy(IContentSecurityPolicyReflectedXss csp)
        {
            string source = String.Empty;
            if (csp != null)
            {
                if (!String.IsNullOrEmpty(csp.Mode))
                    source = csp.Name + " " + csp.Mode + ";";                
            }
            return source;
        }

        string GenerateReportUri(string reportUri) => GenerateString("report-uri {0};", reportUri);

        string GenerateBaseUri(string baseUri) => GenerateString("base-uri {0};", baseUri);

        string GeneratePluginTypes(string pluginTypes) => GenerateString("plugin-types {0};", pluginTypes);

        string GenerateString(string formattedString, string val) => !String.IsNullOrEmpty(val) ? String.Format(formattedString, val) : String.Empty;

        string GenerateBlockAllMixedContent(bool blockAllMixedContent) => (blockAllMixedContent) ? "block-all-mixed-content;" : String.Empty;

        string GenerateUpgradeInsecureRequests(bool upgradeInsecureRequests) => (upgradeInsecureRequests) ? "upgrade-insecure-requests;" : String.Empty;

        public string Create() => CreatePolicy();
    }
}
