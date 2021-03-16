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
        }
    }
}
