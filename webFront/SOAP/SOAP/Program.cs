using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAP
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator.CalculatorSoap c = new Calculator.CalculatorSoapClient();
            Console.WriteLine(c.Add(2, 11));
            Console.ReadLine();

            /*MathsOperations.MathsOperationsClient m = new MathsOperations.MathsOperationsClient();
            Console.WriteLine(m.Add(2, 56));
            Console.ReadLine();*/

            WebProxyService.IWebProxyService w = new WebProxyService.WebProxyServiceClient();
            Console.Write(w.GetAllStations());
        }
    }
}
