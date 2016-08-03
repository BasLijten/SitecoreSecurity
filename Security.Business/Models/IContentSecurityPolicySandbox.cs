using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Business.Models
{
    public interface IContentSecurityPolicySandbox
    {
        bool Enable { get; set; }
        bool AllowForms { get; set; }
        bool AllowSameOrigin { get; set; }
        bool AllowScripts { get; set; }
        bool AllowTopNavigation { get; set; }
        bool AllowPopups { get; set; }
        bool AllowPointerLock { get; set; }
    }
}
