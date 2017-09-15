using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Sepidrah.Web.Core.Models;

namespace Sepidrah.Web.APIs.Auth
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidateAppAttribute : AuthorizeAttribute
    {
        bool IsAuth = false;


        public override void OnAuthorization(HttpActionContext actionContext)
        {



            IEnumerable<string> AppKey;
            IEnumerable<string> AppHash;
            var IsOK = actionContext.Request.Headers.TryGetValues("apikey", out AppKey);
            var IsOKHash = actionContext.Request.Headers.TryGetValues("apihash", out AppHash);




            if (!IsOK || !IsOKHash)
            {
                var err = new List<ErrorBase>();
                err.Add(ErrorBase.AppNotValid);
                actionContext.Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseBase() { Status = Status.Faiure, Error = err }))




                };

                return;
            }

            var APIKey = AppKey.FirstOrDefault();
            var APIHash = AppHash.FirstOrDefault();

            if (!(Core.Security.Certification.IsAppValid(APIKey, APIHash)))
            {
                var err = new List<ErrorBase>();
                err.Add(ErrorBase.AppNotValid);
                actionContext.Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseBase() { Status = Status.Faiure, Error = err }))
                };

                return;
            }




            IsAuth = true;


            base.OnAuthorization(actionContext);


        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {

            return IsAuth;
        }
    }
}