using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BasicServerHTTPlistener
{
    public class Header
    {
        public Header()
        {

        }


        public string getHeader(HttpListenerRequest request)
        {
            return request.Headers.ToString();
        }
    }

    
}
