namespace Security.Business.Models
{
    public interface IContentSecurityPolicyReferrer
    {
        bool NoRefferer { get; set; }
        bool NoReffererWhenDowngrade { get; set; }
        bool Origin { get; set; }
        bool OriginWhenCrossOrigin { get; set; }
        bool UnsafeUrl { get; set; }
    }
}