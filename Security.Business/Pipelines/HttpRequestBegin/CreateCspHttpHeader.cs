using Security.Business.Constants;
using Security.Business.Models;
using Security.Business.Models.ContentSecurityPolicy;
using Security.Business.Models.ContentSecurityPolicySources;
using Sitecore;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Layouts;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.Sites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Security.Business.Pipelines.HttpRequestBegin
{
    public class CreateCspHttpHeader : HttpRequestProcessor
    {
        private const string ContentSecurityPolicyHeader = "Content-Security-Policy";
        private const string XContentSecurityPolicyHeader = "X-Content-Security-Policy";
        private const string ContentSecurityPolicyHeaderReportOnly = "Content-Security-Policy-Report-Only";
        private const string XContentSecurityPolicyHeaderReportOnly = "X-Content-Security-Policy-Report-Only";
        private const string XFrameOptionsHeader = "X-Frame-Options";
        private const string XXssProtection = "X-XSS-Protection";
        public override void Process(HttpRequestArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            string rawUrl = args.Context.Request.RawUrl;
            if (rawUrl.StartsWith("/sitecore", StringComparison.InvariantCultureIgnoreCase) || (!Sitecore.Context.PageMode.IsNormal || Sitecore.Context.PageMode.IsPreview))
            {
                return;
            }

            CreateHeaders(args);
        }

        private void CreateHeaders(HttpRequestArgs args)
        {
            var currentItem = Context.Item;
            if (currentItem != null)
            {
                var headerPolicy = CreatePolicy(currentItem, CspFieldIds.CspLinkFieldId);
                var reportPolicy = CreatePolicy(currentItem, CspFieldIds.CspLinkReportOnlyFieldId);

                CreateAppliedCSP(headerPolicy, args);
                CreateReportCSP(reportPolicy, args);
                CreateXFrameOptions(headerPolicy, args);
                CreateXssProtection(headerPolicy.ReflectedXss, args);
            }
        }

        private void BuildCSP(string policyValue, string header, HttpRequestArgs args)
        {
            try
            {
                {
                    if (!String.IsNullOrEmpty(policyValue))
                    {
                        args.Context.Response.Headers.Add(header, policyValue);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Info("CSP policyValue not valid, CSP not applied", this);
            }
        }

        private void CreateAppliedCSP(IContentSecurityPolicy policy, HttpRequestArgs args)
        {
            if (policy != null)
            {
                var policyValue = policy.ToString();
                BuildCSP(policyValue, ContentSecurityPolicyHeader, args);
                BuildCSP(policyValue, XContentSecurityPolicyHeader, args);
            }
        }

        private void CreateReportCSP(IContentSecurityPolicy policy, HttpRequestArgs args)
        {
            if (policy != null)
            {
                var policyValue = policy.ToString();
                BuildCSP(policyValue, ContentSecurityPolicyHeaderReportOnly, args);
                BuildCSP(policyValue, XContentSecurityPolicyHeaderReportOnly, args);
            }
        }

        private void CreateXFrameOptions(IContentSecurityPolicy policy, HttpRequestArgs args)
        {
            if (policy != null)
            {
                if (policy.FrameAncestors != null)
                {
                    CreateXFrameOptionsHeaderBasedOnPolicySource(policy.FrameAncestors, args);
                }
                // fall back to Default policy
                else if(policy.Default !=null)
                {
                    CreateXFrameOptionsHeaderBasedOnPolicySource(policy.Default, args);
                }            
                // if nothing is set, default back to default setting: Sameorigin
                else
                {
                    args.Context.Response.Headers.Add(XFrameOptionsHeader, "SAMEORIGIN");
                }
            }
        }

        private void CreateXFrameOptionsHeaderBasedOnPolicySource(IContentSecurityPolicySource source, HttpRequestArgs args)
        {
            if (source.Options.None)
                args.Context.Response.Headers.Add(XFrameOptionsHeader, "DENY");
            else if (source.Options.Self)
                args.Context.Response.Headers.Add(XFrameOptionsHeader, "SameOrigin");
            else if (String.IsNullOrEmpty(source.Hostnames))
                args.Context.Response.Headers.Add(XFrameOptionsHeader, $"Allow-FROM {source.Hostnames}");            
        }

        private void CreateXssProtection(IContentSecurityPolicyReflectedXss policy, HttpRequestArgs args)
        {
            string xssmode = String.Empty;
            if(policy != null)
            {
                if(!String.IsNullOrEmpty(policy.Mode))
                {
                    switch(policy.Mode.ToLower())
                    {
                        case "allow":
                            xssmode = "0";
                            break;                        
                        case "filter":
                            xssmode = "1";
                            break;
                        case "block":                            
                        default:
                            xssmode = "1; mode=block";
                            break;
                    }
                    args.Context.Response.Headers.Add(XXssProtection, xssmode);
                }
            }
        }

        private IContentSecurityPolicy CreatePolicy(Item currentItem, string fieldId)
        {
            IContentSecurityPolicy policy = null;
            var cspField = (LookupField)currentItem.Fields[fieldId];
            if (cspField != null)
            {
                var cspItem = cspField.TargetItem;
                policy = Map(cspItem);
            }
            return policy;
        }

        private IContentSecurityPolicy Map(Item i)
        {
            IContentSecurityPolicy p = new ContentSecurityPolicy();
            p.Default = GetDefaultSecurityPolicy(i);
            p.Script = GetScriptSecurityPolicy(i);
            p.Style = GetStyleSecurityPolicy(i);
            p.Image = GetImageSecurityPolicy(i);
            p.Font = GetFontSecurityPolicy(i);
            p.Connect = GetConnectSecurityPolicy(i);
            p.Media = GetMediaSecurityPolicy(i);
            p.Object = GetObjectSecurityPolicy(i);
            p.Child = GetChildSecurityPolicy(i);
            p.FrameAncestors = GetFrameAncestorsSecurityPolicy(i);
            p.FormAction = GetFormActionSecurityPolicy(i);
            p.Manifest = GetManifestSecurityPolicy(i);

            //p.Referrer = GetReferrerOptions(i);
            //p.Sandbox = GetSandboxOptions(i);
            p.ReflectedXss = GetReflectedXssOptions(i);

            p.BaseUri = GetBaseUri(i);
            p.BlockAllMixedContent = GetMixedContentSetting(i);
            p.PluginTypes = GetPluginTypes(i);
            p.ReportOnly = GetReportOnlyOption(i);
            p.ReportUri = GetReportUri(i);
            p.UpgradeInsecureRequests = GetUpgradeInsecureRequestsOptions(i);

            return p;
        }

        private IContentSecurityPolicyReflectedXss GetReflectedXssOptions(Item i)
        {
            string fieldName = CspFieldIds.ReflextedXssSourceFieldId;
            IContentSecurityPolicyReflectedXss reflectedXss = new ReflectiveXssContentSecurityPolicySource();            
            var optionsField = i.Fields[fieldName];
            var listField = (MultilistField)optionsField;
            var options = listField.GetItems();

            foreach (var option in options)
            {
                switch (option.Name.ToLower())
                {
                    case "allow":
                        reflectedXss.Mode = "allow";
                        break;
                    case "filter":
                        reflectedXss.Mode = "filter";
                        break;
                    case "block":
                        reflectedXss.Mode = "block";
                        break;
                    default:
                        break;
                }
            }
           

            return reflectedXss;            
        }
        

        private string GetPluginTypes(Item i)
        {
            string fieldName = CspFieldIds.PluginTypesFieldId;
            return GetString(i, fieldName);
        }

        private bool GetUpgradeInsecureRequestsOptions(Item i)
        {
            string field = CspFieldIds.UpgradeInsecureRequestsFieldId;
            return GetBoolean(i, field);
        }

        private bool GetReportOnlyOption(Item i)
        {
            string field = CspFieldIds.ReportOnlyFieldId;
            return GetBoolean(i, field);
        }
        private bool GetMixedContentSetting(Item i)
        {
            string field = CspFieldIds.BlockAllMixedContextFieldId;
            return GetBoolean(i, field);
        }

        private string GetReportUri(Item i)
        {
            string fieldName = CspFieldIds.ReportUriFieldId;
            return GetString(i, fieldName);
        }



        private IContentSecurityPolicySandbox GetSandboxOptions(Item i)
        {
            string fieldName = CspFieldIds.SandboxSourceFieldId;
            throw new NotImplementedException();
        }

        private IContentSecurityPolicyReferrer GetReferrerOptions(Item i)
        {
            string fieldName = CspFieldIds.ReportOnlyFieldId;
            throw new NotImplementedException();
        }



        private string GetBaseUri(Item i)
        {
            string fieldName = CspFieldIds.BaseUriFieldId;
            return GetString(i, fieldName);
        }

        private IContentSecurityPolicySource GetDefaultSecurityPolicy(Item i)
        {
            string optionsFieldname = CspFieldIds.CspDefaultSource;
            string hostnameFieldname = CspFieldIds.CspDefaultHost;

            return GetSourceSettings<DefaultContentSecurityPolicySource>(i, optionsFieldname, hostnameFieldname);
        }

        private IContentSecurityPolicySource GetScriptSecurityPolicy(Item i)
        {
            string optionsFieldname = CspFieldIds.CspScriptSource;
            string hostnameFieldname = CspFieldIds.CspScriptHost;

            return GetSourceSettings<ScriptContentSecurityPolicySource>(i, optionsFieldname, hostnameFieldname);
        }

        private IContentSecurityPolicySource GetStyleSecurityPolicy(Item i)
        {
            string optionsFieldname = CspFieldIds.CspStyleSource;
            string hostnameFieldname = CspFieldIds.CspStyleHost;

            return GetSourceSettings<StyleContentSecurityPolicySource>(i, optionsFieldname, hostnameFieldname);
        }



        private IContentSecurityPolicySource GetImageSecurityPolicy(Item i)
        {
            string optionsFieldname = CspFieldIds.CspImageSource;
            string hostnameFieldname = CspFieldIds.CspImageHost;

            return GetSourceSettings<ImageContentSecurityPolicySource>(i, optionsFieldname, hostnameFieldname);
        }

        private IContentSecurityPolicySource GetFontSecurityPolicy(Item i)
        {
            string optionsFieldname = CspFieldIds.CspFontSource;
            string hostnameFieldname = CspFieldIds.CspFontHost;

            return GetSourceSettings<FontContentSecurityPolicySource>(i, optionsFieldname, hostnameFieldname);
        }

        private IContentSecurityPolicySource GetConnectSecurityPolicy(Item i)
        {
            string optionsFieldname = CspFieldIds.CspConnectSource;
            string hostnameFieldname = CspFieldIds.CspConnectHost;

            return GetSourceSettings<ConnectContentSecurityPolicySource>(i, optionsFieldname, hostnameFieldname);
        }

        private IContentSecurityPolicySource GetMediaSecurityPolicy(Item i)
        {
            string optionsFieldname = CspFieldIds.CspMediaSource;
            string hostnameFieldname = CspFieldIds.CspMediaHost;

            return GetSourceSettings<MediaContentSecurityPolicySource>(i, optionsFieldname, hostnameFieldname);
        }

        private IContentSecurityPolicySource GetObjectSecurityPolicy(Item i)
        {
            string optionsFieldname = CspFieldIds.CspObjectSource;
            string hostnameFieldname = CspFieldIds.CspObjectHost;

            return GetSourceSettings<ObjectContentSecurityPolicySource>(i, optionsFieldname, hostnameFieldname);
        }

        private IContentSecurityPolicySource GetChildSecurityPolicy(Item i)
        {
            string optionsFieldname = CspFieldIds.CspChildSource;
            string hostnameFieldname = CspFieldIds.CspChildHost;

            return GetSourceSettings<ChildContentSecurityPolicySource>(i, optionsFieldname, hostnameFieldname);
        }

        private IContentSecurityPolicySource GetFrameAncestorsSecurityPolicy(Item i)
        {
            string optionsFieldname = CspFieldIds.CspFrameSource;
            string hostnameFieldname = CspFieldIds.CspFrameHost;

            return GetSourceSettings<FrameAncestorContentSecurityPolicySource>(i, optionsFieldname, hostnameFieldname);
        }

        private IContentSecurityPolicySource GetFormActionSecurityPolicy(Item i)
        {
            string optionsFieldname = CspFieldIds.CspFormSource;
            string hostnameFieldname = CspFieldIds.CspFormHost;

            return GetSourceSettings<FormActionContentSecurityPolicySource>(i, optionsFieldname, hostnameFieldname);
        }

        private IContentSecurityPolicySource GetManifestSecurityPolicy(Item i)
        {
            string optionsFieldname = CspFieldIds.CspManifestSource;
            string hostnameFieldname = CspFieldIds.CspManifestHost;

            return GetSourceSettings<ManifestContentSecurityPolicySource>(i, optionsFieldname, hostnameFieldname);
        }


        private IContentSecurityPolicySource GetSourceSettings<TContentSecurityPolicySource>(Item csp, string optionsFieldname, string hostnameFieldname) where TContentSecurityPolicySource : class, IContentSecurityPolicySource, new()
        {
            IContentSecurityPolicySource source = null;
            if (csp != null && csp.Fields[optionsFieldname] != null)
            {
                source = new TContentSecurityPolicySource();
                var optionsField = csp.Fields[optionsFieldname];
                var listField = (MultilistField)optionsField;
                var options = listField.GetItems();
                foreach (var option in options)
                {
                    switch (option.Name)
                    {
                        case "None":
                            source.Options.None = true;
                            return source;
                        case "All":
                            source.Options.All = true;
                            break;
                        case "Self":
                            source.Options.Self = true;
                            break;
                        case "Data":
                            source.Options.Data = true;
                            break;
                        case "Unsafe Inline":
                            source.Options.UnsafeInline = true;
                            break;
                        case "Unsafe Eval":
                            source.Options.UnsafeEval = true;
                            break;
                        default:
                            Log.Warn(String.Format("Content Security Policy returned unrecognized value: {0}", option.Name), this);
                            break;
                    }
                }
                var hostnames = String.Empty;

                if (optionsField != null)
                {
                    if (csp.Fields[hostnameFieldname] != null)
                        hostnames = csp.Fields[hostnameFieldname].Value;
                }
                source.Hostnames = hostnames;
            }
            return source;
        }

        private bool GetBoolean(Item csp, string fieldName)
        {
            bool result = false;
            if (csp != null && csp.Fields[fieldName] != null)
            {
                var optionsField = (CheckboxField)csp.Fields[fieldName];
                result = optionsField.Checked;
            }
            return result;
        }

        private string GetString(Item csp, string fieldName)
        {
            string result = String.Empty;
            if (csp != null && csp.Fields[fieldName] != null)
            {
                result = csp.Fields[fieldName].Value;

            }
            return result;
        }
    }
}
