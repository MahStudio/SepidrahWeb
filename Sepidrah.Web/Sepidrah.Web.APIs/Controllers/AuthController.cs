using Sepidrah.Web.APIs.Auth;
using Sepidrah.Web.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Sepidrah.Web.APIs.Controllers
{
    [RoutePrefix("V0/auth")]
    public class AuthController : ApiController
    {
        [Route("signup")]
        [ValidateApp]
        [HttpPost]
        public async Task<IHttpActionResult> SignUp(RegisterVM user)
        {

            if (user == null)
            {
                var err = new List<ErrorBase>();
                err.Add(ErrorBase.SyntaxError);

                return Content(HttpStatusCode.BadRequest, new ResponseBase() { Error = err, Status = Status.Faiure });
            }

            if (!(user.IsValid().Status == Status.OK))
            {

                return Content(HttpStatusCode.BadRequest, user.IsValid());
            }

            var usr = new Core.BL.UserBL();
            var r = await usr.CreateUser(user.Email, user.Password, user.Name, user.LastName);
            if (r.Status == Status.OK)
                return Content(HttpStatusCode.Accepted, new ResponseBase()
                {
                    Status = Status.OK,
                    Data = new
                    {
                        token = usr.GenerateToken(user.Email),
                        user = await usr.GetUserLevelOne(user.Email)

                    }
                });
            else
            {

                return Content(HttpStatusCode.Conflict, r);

            }


        }
    }
}
