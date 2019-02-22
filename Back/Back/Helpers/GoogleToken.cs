using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Helpers
{
    public class GoogleToken
    {
        public string access_token;
        public int expires_in;
        public string refresh_token;
        public string scope;
        public string token_type;
        public string id_token;
    }
}
