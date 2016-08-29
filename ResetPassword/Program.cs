using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace ResetPassword
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Security.Cryptography.CryptoConfig.AddAlgorithm(typeof(Zetetic.Security.Pbkdf2Hash), "pbkdf2_local");

            string userId = @"sitecore\admin";
            string passwd = String.Empty;
            MembershipUser user = Membership.GetUser(userId, false);
            user.ResetPassword();

            Console.WriteLine(passwd);
            Console.ReadLine();
        }
    }
}
