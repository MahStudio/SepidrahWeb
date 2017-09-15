using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepidrah.Web.Core.Models
{
    class UserCredencials
    {
        private string _type = "user::credencial";
        private int _schemaver = 0;

        [JsonProperty("type")]
        public string Type { get { return _type; } }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("schemaVer")]
        public int SchemaVer { get { return _schemaver; } }
    }
}
