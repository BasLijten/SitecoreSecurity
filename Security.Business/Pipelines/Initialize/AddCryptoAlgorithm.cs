using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Business.Pipelines.Initialize
{
    public class AddCryptoAlgorithms
    {
        public void Process(PipelineArgs args)
        {
            //adds the useable crypte hashing algorithm pbkdf2_local
            System.Security.Cryptography.CryptoConfig.AddAlgorithm(typeof(Zetetic.Security.Pbkdf2Hash), "pbkdf2_local");
        }
    }
}
