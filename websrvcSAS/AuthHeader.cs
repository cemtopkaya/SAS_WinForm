using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

namespace websrvcSAS
{
    public class AuthHeader : SoapHeader
    {
        public string Username;
        public string Password;

        public AuthHeader()
        {
            
        }
        public AuthHeader(string _sUserName, string _sPass)
        {
            this.Username = _sUserName;
            this.Password = _sPass;
        }
    }
}