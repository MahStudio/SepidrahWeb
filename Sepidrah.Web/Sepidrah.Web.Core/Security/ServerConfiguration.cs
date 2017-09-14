using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepidrah.Web.Core.Security
{
    class ServerConfiguration
    {
        internal ServerConfiguration GetValues()
        {
            return Constants.Secret();
        }
        public string Bucket { get; set; }
        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string JWTSecret { get; set; }
    }
}
