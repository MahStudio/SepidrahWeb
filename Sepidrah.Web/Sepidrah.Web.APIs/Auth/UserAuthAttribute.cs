using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using System.Web.Http;
using System.Web.Http.Controllers;
using Sepidrah.Web.Core.Models;

namespace Sepidrah.Web.APIs.Auth
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UserAuthAttribute : AuthorizeAttribute
    {
        bool IsAuth = false;


        public override void OnAuthorization(HttpActionContext actionContext)
        {


            IEnumerable<string> headerValues;
            var IsOK = actionContext.Request.Headers.TryGetValues("Token", out headerValues);





            if (!IsOK)
            {
                var err = new List<ErrorBase>();
                err.Add(ErrorBase.TokenNotValid);
                actionContext.Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseBase() { Status = Status.Faiure, Error = err }))




                };

                return;
            }

            var Token = headerValues.FirstOrDefault();
            var usr = new Core.BL.UserBL();

            var Validation = usr.IsTokenValid(Token);
            if (!(Validation.Status == Status.OK))
            {

                actionContext.Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent(JsonConvert.SerializeObject(Validation))




                };
                return;
            }


            IsAuth = true;

            actionContext.ActionArguments["email"] = Validation.Data;
            base.OnAuthorization(actionContext);


        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {

            return IsAuth;
        }
    }
}