using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase.Configuration.Client;
using Couchbase;
using Couchbase.Authentication;
using Couchbase.Core;
using Sepidrah.Web.Core.Security;

namespace Sepidrah.Web.Core
{
    internal static class DBConnect
    {
        public static void DoConfig()
        {
            var config = new ServerConfiguration().GetValues();
            ClusterHelper.Initialize(new ClientConfiguration
            {
                Servers = new List<Uri> { new Uri(config.Server) }
            });
            ClusterHelper.Get().Authenticate(new PasswordAuthenticator(config.Username, config.Password));

        }
        internal static IBucket GetBucket()
        {


            return ClusterHelper.GetBucket(new ServerConfiguration().GetValues().Bucket);

        }
    }
}
