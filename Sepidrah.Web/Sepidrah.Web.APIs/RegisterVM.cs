using Newtonsoft.Json;
using Sepidrah.Web.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sepidrah.Web.APIs
{
    public class RegisterVM
    {
        //Email
        //pwssword
        //name
        //lastname
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("pass")]
        public string Password { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        public ResponseBase IsValid()
        {

            var R = new ResponseBase();
            R.Error = new List<ErrorBase>();
            R.Status = Status.OK;
            if (string.IsNullOrEmpty(Email))
            {

                R.Status = Status.Faiure;
                R.Error.Add(ErrorBase.UserEmailNotValid);

            }
            if (string.IsNullOrEmpty(Name))
            {
                R.Error.Add(ErrorBase.NameNull);
                R.Status = Status.Faiure;
            }
            if (string.IsNullOrEmpty(Password))
            {
                R.Error.Add(ErrorBase.UserPasswordNotValid);
                R.Status = Status.Faiure;
            }
            return R;

        }
    }
}