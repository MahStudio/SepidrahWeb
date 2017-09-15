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
using Sepidrah.Web.Core.Models;

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
        internal static async Task<ResponseBase> CreateLookUpAsync<T>(T Data, string key)
        {


            var document = new Document<T>
            {
                Id = key,
                Content = Data
            };

            var upsert = await GetBucket().InsertAsync(document);
            if (upsert.Success)
            {
                return new ResponseBase() { Status = Status.OK };
            }
            else
            {
                var err = new List<ErrorBase>();
                err.Add(ErrorBase.DatabaseInsertError);
                return new ResponseBase() { Status = Status.Faiure, Error = err };



            }
        }
        internal static async Task<ResponseBase> UpdateLookup<T>(T Data, string key)
        {
            var document = new Document<T>
            {
                Id = key,
                Content = Data
            };

            var upsert = GetBucket().Replace<T>(document);
            if (upsert.Success)
            {
                return new ResponseBase() { Status = Status.OK };
            }
            else
            {
                var err = new List<ErrorBase>();
                err.Add(ErrorBase.DatabaseUpdateError);
                return new ResponseBase() { Status = Status.Faiure, Error = err };
            }
        }
    }
}
