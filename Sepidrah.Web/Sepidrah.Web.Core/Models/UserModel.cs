using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepidrah.Web.Core.Models
{
    class UserModel
    {
        private string _type = "user";
        private int _schemaver = 0;

        public string Type { get { return _type; } }
        public int Key { get; set; }
        public DateTime CreatedAt { get; set; }
        public int SchemaVer { get { return _schemaver; } }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
