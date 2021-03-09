using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BasicHeader
{
    class Header
    {
        public string getHeader(HttpListenerRequest request)
        {
            return request.Headers.ToString();
        }

        public long getBodyLength(HttpListenerRequest request)
        {
            return request.ContentLength64;
        }

        public bool IsLocal(HttpListenerRequest request)
        {
            return request.IsLocal;
        }
    }
}
