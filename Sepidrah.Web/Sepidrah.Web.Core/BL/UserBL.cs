using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sepidrah.Web.Core.Models;

namespace Sepidrah.Web.Core.BL
{
    public class UserBL
    {
        public async Task<ResponseBase> CreateUser(string email, string passhash, string firstname, string lastname)
        {
            var usr = new UserModel()
            {
                Name = firstname,
                LastName = lastname,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt=DateTime.UtcNow,
                Email=email,

            };

            return new ResponseBase(){Status = Status.OK};
        }
        public async Task<ResponseBase> CreateCredencials(int userKey, string passhash)
        {
            

            return new ResponseBase() { Status = Status.OK };
        }
        public async Task<ResponseBase> CreateEmailLookup(int userKey, string email)
        {


            return new ResponseBase() { Status = Status.OK };
        }

    }
}
