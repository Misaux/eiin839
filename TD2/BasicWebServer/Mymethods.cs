using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace BasicServerHTTPlistener
{
    class Mymethods
    {
        public Mymethods()
        {
            
        }

        public string mymethod(string param1, string param2)
        {
            return "<HTML><BODY> Hello " + param1 + " et " + param2 + "</BODY></HTML>";
        }
    }
}
