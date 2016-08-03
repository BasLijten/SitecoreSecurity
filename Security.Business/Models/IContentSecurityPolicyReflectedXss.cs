using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Business.Models
{
    public interface IContentSecurityPolicyReflectedXss
    {
        bool Allow { get; set; }
        bool Block { get; set; }
        bool Filter { get; set; }
    }
}
