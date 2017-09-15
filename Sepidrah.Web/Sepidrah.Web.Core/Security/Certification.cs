using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepidrah.Web.Core.Security
{
    public class Certification
    {
        public static bool IsAppValid(string AppID, string ApiHash)
        {
            if (AppID == "Sepidrah_UWP" && ApiHash == "53d6754fb254783d26f8b3224c6719897159420aa33ab3e439306af8c1e9c464")
            {
                return true;
            }

            return false;
        }
    }
}
