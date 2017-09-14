using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepidrah.Web.Core.Models
{
    public class ResponseBase
    {
        private List<ErrorBase> _error = new List<ErrorBase>();
        [JsonProperty("status")]
        public Status Status { get; set; }
        [JsonProperty("error")]
        public List<ErrorBase> Error
        {
            get
            {
                return _error;
            }
            set
            {
                _error = value;
            }
        }
        [JsonProperty("data")]
        public object Data { get; set; }
    }
    public enum Status
    {
        OK = 0,
        Faiure = 1,
        Warned = 2
    }
    public enum ErrorBase
    {
        //Root errors
        None = 0,
        FunctionDisabled = 1,
        SyntaxError = 2,
        UnExpectedError = 3,
        TokenNotValid = 4,
        ValueNotRecognized = 5,
        AppNotValid = 6,
        RequestSpam = 7,


        //UserErrors
        UserEmailNotValid = 203,
        UserPasswordNotValid = 205,
        NameNull = 206,
        EmailConflict = 208,
        PasswordWrong = 210,
        EmailNotExist = 212,
        UserNotFound = 213,
    }
}
