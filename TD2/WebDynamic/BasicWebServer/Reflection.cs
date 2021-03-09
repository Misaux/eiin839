using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer
{
    class Reflection
    {
        public string reflection(string arg1, string arg2)
        {
            return ("<HTML><BODY>Using method reflection : Hello " + arg1 + " et " + arg2 + "</BODY></HTML>");
        }
        public string reflectionSecond(string arg1, string arg2)
        {
            return ("<HTML><BODY>Using method reflectionSecond : Hello " + arg1 + " et " + arg2 + "</BODY></HTML>");
        }
    }
}
