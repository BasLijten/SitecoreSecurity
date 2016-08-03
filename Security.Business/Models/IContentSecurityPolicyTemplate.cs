namespace Security.Business.Models
{
    public interface IContentSecurityPolicy
    {
        IContentSecurityPolicySource Default { get; set; }
        IContentSecurityPolicySource Script { get; set; }
        IContentSecurityPolicySource Style { get; set; }

        IContentSecurityPolicySource Image { get; set; }

        IContentSecurityPolicySource Font { get; set; }

        IContentSecurityPolicySource Connect { get; set; }

        IContentSecurityPolicySource Media { get; set; }

        IContentSecurityPolicySource Object { get; set; }

        IContentSecurityPolicySource Child { get; set; }

        IContentSecurityPolicySource FrameAncestors { get; set; }

        IContentSecurityPolicySource FormAction { get; set; }
        IContentSecurityPolicySource Manifest { get; set; }

        bool UpgradeInsecureRequests { get; set; }
        bool BlockAllMixedContent { get; set; }
        string BaseUri { get; set; }
        string PluginTypes { get; set; }
        bool ReportOnly { get; set; }
        string ReportUri { get; set; }

        IContentSecurityPolicySandbox Sandbox { get; set; }

        IContentSecurityPolicyReflectedXss ReflectedXss { get; set; }

        IContentSecurityPolicyReferrer Referrer { get; set; }

    }
}