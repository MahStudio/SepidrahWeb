using Newtonsoft.Json;
using Sepidrah.Web.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Sepidrah.Web.APIs.Auth
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FunctionDisabledAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var a = new List<ErrorBase>();
            a.Add(ErrorBase.FunctionDisabled);
            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Forbidden,
                Content = new StringContent(JsonConvert.SerializeObject(new ResponseBase() { Status = Status.Faiure, Error = a }))

            };
            return;
            //base.OnAuthorization(actionContext);
        }
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {

            return false;
        }

    }
}