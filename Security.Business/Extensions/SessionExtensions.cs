using Sitecore.SessionManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Security.Business.Extensions
{
    public static class SessionExtensions
    {
        public static void RotateSessionID(this Controller controller)
        {
            controller.Session.Abandon();
            controller.Response.Cookies.Remove("ASP.NET_SessionId");
            var sidm = new ConditionalSessionIdManager();
            var a = sidm.CreateSessionID(controller.HttpContext.ApplicationInstance.Context);
            bool redirected = false;
            bool cookieadded = false;
            sidm.SaveSessionID(controller.HttpContext.ApplicationInstance.Context, a, out redirected, out cookieadded);
        }
    }
}
