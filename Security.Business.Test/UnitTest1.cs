using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Security.Business.Models;
using System.Collections.Generic;
using Security.Business.Models.ContentSecurityPolicySources;

namespace Security.Business.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ContentSecurityPolicy policy = new ContentSecurityPolicy();
            policy.Default.Hostnames = "www.google.com";
            policy.Default.Options = new ContentSecurityPolicyOptions()
            {
                Self = true,
                Data = true
            };            
            var result = policy.ToString();
            
;        }

        [TestMethod]
        public void Test2()
        {
            IContentSecurityPolicy policy = new ContentSecurityPolicy();           

            var a = GetSourceSettings<DefaultContentSecurityPolicySource>(); 
            var b = GetSourceSettings<ScriptContentSecurityPolicySource>();

            var result = policy.ToString();
        }

       
        private IContentSecurityPolicySource GetSourceSettings<TContentSecurityPolicySource>() where TContentSecurityPolicySource : class, IContentSecurityPolicySource, new()
        {
            IContentSecurityPolicySource source = null;
               source = new TContentSecurityPolicySource();
                
           
            //foreach (var option in options)
            //    {
            //        switch (option.Name)
            //        {
            //            case "None":
            //                source.Options.None = true;
            //                return source;
            //            case "All":
            //                source.Options.All = true;
            //                break;
            //            case "Self":
            //                source.Options.Self = true;
            //                break;
            //            case "Data":
            //                source.Options.Data = true;
            //                break;
            //            case "Unsafe Inline":
            //                source.Options.UnsafeInline = true;
            //                break;
            //            case "Unsafe Eval":
            //                source.Options.UnsafeEval = true;
            //                break;
            //            default:                            
            //                break;
            //        }                               
            //}
            return source;
        }
    }

}
