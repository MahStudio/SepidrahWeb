using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sepidrah.Web.Core.Models;
using Couchbase.Core;
using Sepidrah.Web.Core.Util;
using JWT;
using Sepidrah.Web.Core.Security;

namespace Sepidrah.Web.Core.BL
{
    public class UserBL
    {
        private readonly IBucket _bucket = DBConnect.GetBucket();
        public async Task<ResponseBase> CreateUser(string email, string passhash, string firstname, string lastname)
        {
            var usr = new UserModel()
            {
                Name = firstname,
                LastName = lastname,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt=DateTime.UtcNow,
                Email=email.ToLower(),

            };
            usr.Key = int.Parse(_bucket.Increment("UserIncrement").Value.ToString());
            if (await IsEmailExistsAsync(usr.Email))
            {
                var err = new List<ErrorBase>();
                err.Add(ErrorBase.EmailConflict);
                return new ResponseBase() { Status = Status.Faiure, Error = err };

            }
            var R = await DBConnect.CreateLookUpAsync<UserModel>(usr, Utils.KeyMaker(usr.Type, usr.Key));
            await CreateCredencials(usr.Key, passhash);
            await CreateEmailLookup(usr.Key, email.ToLower());
            await CreateProfileInfo(usr.Key);

            return new ResponseBase(){Status = Status.OK};
        }
        public async Task<ResponseBase> CreateCredencials(int userKey, string passhash)
        {

            var a = new UserCredencials() { Password = passhash, UpdatedAt = DateTime.Now };
            await DBConnect.CreateLookUpAsync<UserCredencials>(a, Utils.KeyMaker(a.Type, userKey));
            return new ResponseBase() { Status = Status.OK };
        }
        public async Task<ResponseBase> CreateEmailLookup(int userKey, string email)
        {
            var a = new UserEmailLookupModel() { UserKey = userKey };
            await DBConnect.CreateLookUpAsync<UserEmailLookupModel>(a, Utils.KeyMaker(a.Type, email));

            return new ResponseBase() { Status = Status.OK };
        }
        public async Task<ResponseBase> CreateProfileInfo(int userKey)
        {
            var Z = new UserDetailModel()
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Owner = userKey,
            };
            var R = await DBConnect.CreateLookUpAsync<UserDetailModel>(Z, Utils.KeyMaker(Z.Type, userKey));
            return R;
        }
        public async Task<bool> IsEmailExistsAsync(string Email)
        {
            if (await _bucket.ExistsAsync(Utils.KeyMaker(new UserEmailLookupModel().Type, Email)))
            {
                return true;
            }
            else
                return false;
        }
        public async Task<ResponseBase> GetUserKeyByEmail(string email)
        {
            var User = _bucket.Get<UserEmailLookupModel>(Utils.KeyMaker(new UserEmailLookupModel().Type, email.ToLower())).Value;
            if (User == null)
            {
                var R = new ResponseBase() { Status = Status.Faiure };
                R.Error.Add(ErrorBase.UserNotFound);
                return R;
            }
            var Rx = new ResponseBase() { Status = Status.OK, Data= User.UserKey };
            
            return Rx ;
        }
        #region Authentication
        internal async Task<UserCredencials> GetUserCredencial(int UserKey)
        {
            return _bucket.Get<UserCredencials>(Utils.KeyMaker(new UserCredencials().Type, UserKey)).Value;
        }
        public async Task<ResponseBase> AuthenticateByEmail(string email, string pasword)
        {
            var key = await GetUserKeyByEmail(email);

            var Err = new List<ErrorBase>();
            if (key.Data == null)
            {
                Err.Add(ErrorBase.EmailNotExist);
                return new ResponseBase() { Error = Err, Status = Status.Faiure };
            }
            var a = await GetUserCredencial((int)key.Data);
            if (!(pasword == a?.Password))
            {
                Err.Add(ErrorBase.PasswordWrong);
                return new ResponseBase() { Error = Err, Status = Status.Faiure };
            }
            return new ResponseBase() { Status = Status.OK };

        }

        

        public ResponseBase IsTokenValid(string Token)
        {
            var err = new List<ErrorBase>();

            if (string.IsNullOrEmpty(Token))
            {
                return new ResponseBase() { };
            }
            Dictionary<string, string> claims;
            try
            {
                claims = JsonWebToken.DecodeToObject<Dictionary<string, string>>(Token, new ServerConfiguration().GetValues().JWTSecret);
            }
            catch
            {
                err.Add(ErrorBase.TokenNotValid);
                return new ResponseBase() { Error = err, Status = Status.Faiure };
            }
            var nowUTC = Utils.ToUnixTime(DateTime.UtcNow);
            var TokenExp = int.Parse(claims["exp"]);
            if (TokenExp < nowUTC)
            {
                err.Add(ErrorBase.TokenNotValid);
                return new ResponseBase() { Error = err, Status = Status.Faiure };
            }
            return new ResponseBase() { Status = Status.OK, Data = claims["email"] }; ;
        }

        public string GenerateToken(string email)
        {




            var claims = new Dictionary<string, string>
            {

                {"exp", Utils.ToUnixTime(DateTime.UtcNow.AddDays(5)).ToString() },
                {"email", email}
            };

            return JsonWebToken.Encode(claims, new ServerConfiguration().GetValues().JWTSecret, JwtHashAlgorithm.HS512);

        }





        #endregion
    }
}
