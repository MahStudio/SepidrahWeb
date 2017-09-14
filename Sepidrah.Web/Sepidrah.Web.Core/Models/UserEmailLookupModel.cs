using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepidrah.Web.Core.Models
{
    class UserEmailLookupModel
    {
        private string _type = "user::email";
        public string Type { get { return _type; } }
        public int UserKey { get; set; }
    }
}
