using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Business.Models
{
    public interface IContentSecurityPolicyReflectedXss
    {
        string Name { get; }
        string Mode { get; set; }
    }
}
