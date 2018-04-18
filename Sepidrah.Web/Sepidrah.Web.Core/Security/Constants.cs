using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepidrah.Web.Core.Security
{
    class Constants
    {
        internal static ServerConfiguration Secret()
        {
            return new ServerConfiguration()
            {
                Bucket = "Sepidrah",
                Server = "http://somewhere",
                Username = "Administrator",
                Password = "password",
                JWTSecret = "SuperSecretFormula"
                

            };
        }
    }
}
