using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveProfiler.BusinessLogic
{
    public class Account
    {
        private Dictionary<string, string> _keys = new Dictionary<string, string>();

        public string AccountName { get; set; }
        public string RefreshToken { get; set; }

        public Dictionary<string, Character> Characters = new Dictionary<string, Character>();

        public string keyId
        {
            get
            {
                return _keys["keyId"];
            }
            set
            {
                _keys["keyId"] = value;
            }
        }
        public string vCode
        {
            get
            {
                return _keys["vCode"];
            }
            set
            {
                _keys["vCode"] = value;
            }
        }
        public Dictionary<string, string> Keys
        {
            get
            {
                return _keys;
            }
        }
    }
}
