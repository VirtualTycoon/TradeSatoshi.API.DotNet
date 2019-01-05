using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoExchange.API
{
    public class ApiEndpoint
    {
        public ApiEndpoint(Uri uri, AuthenticationMethod authenticationMethod = AuthenticationMethod.None)
        {
            Uri = uri;
            AuthenticationMethod = authenticationMethod;
        }

        public ApiEndpoint(string url, AuthenticationMethod authenticationMethod = AuthenticationMethod.None)
        {
            Uri = new Uri(url);
            AuthenticationMethod = authenticationMethod;
        }

        public Uri Uri { get; set; }
        public AuthenticationMethod AuthenticationMethod { get; set; }
        public override string ToString()
        {
            return Uri.ToString();
        }
    }
}
