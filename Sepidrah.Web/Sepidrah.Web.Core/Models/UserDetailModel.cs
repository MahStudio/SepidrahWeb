using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepidrah.Web.Core.Models
{
    public class UserDetailModel
    {
        private string _type = "user::info";
        private int _schemaver = 0;
        [JsonProperty("type")]
        public string Type { get { return _type; } }
        [JsonProperty("key")]
        public int Key { get; set; }
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("schemaVer")]
        public int SchemaVer { get { return _schemaver; } }
        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("owner")]
        public int Owner { get; set; }
        // Now the actual information
        [JsonProperty("birth")]
        public DateTime? Birth { get; set; }
        [JsonProperty("gender")]
        public Gender? Gender { get; set; }
        [JsonProperty("children")]
        public List<DateTime> ChildrenBirth { get; set; }
        //respiratory diseases
        [JsonProperty("respiratory")]
        public List<string> RespiratoryDiseases { get; set; }
        //Heart disease
        [JsonProperty("heart")]
        public List<string> HeartDiseases { get; set; }
    }
    public enum Gender
    {
        Woman,
        Man
    }



}
