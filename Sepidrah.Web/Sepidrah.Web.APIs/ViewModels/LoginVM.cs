using Newtonsoft.Json;
using Sepidrah.Web.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sepidrah.Web.APIs.ViewModels
{
    public class LoginVM
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("pass")]
        public string Password { get; set; }
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
            
            if (string.IsNullOrEmpty(Password))
            {
                R.Error.Add(ErrorBase.UserPasswordNotValid);
                R.Status = Status.Faiure;
            }
            return R;

        }
    }
}