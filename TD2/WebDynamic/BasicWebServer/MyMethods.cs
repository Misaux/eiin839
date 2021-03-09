using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace BasicWebServer
{
    class MyMethods
    { 
        
        public static string mymethod(string param1, string param2)
        {
            return ("<HTML><BODY> Hello " + param1 + " et " + param2 + "</BODY></HTML>");
        }

        public static string mymethodCGI(string arg1, string arg2)
        {
            Console.WriteLine("CGI");
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"D:\Martin\Cours SI4\S8\WS\eiin839\TD2\WebDynamic\ExecTest\bin\Debug\ExecTest.exe"; // Specify exe name.
            start.Arguments = arg1 + " " + arg2; // Specify arguments.
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            //
            // Start the process.
            //
            using (Process process = Process.Start(start))
            {
                //
                // Read in all the text from the process with the StreamReader.
                //
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    return result;
                }
            }
        }
        public static string mymethodReflection(string methodName, string arg1, string arg2)
        {
            methodName = methodName.Remove(methodName.Length - 1, 1);
            Console.WriteLine(methodName);
            Type type = typeof(Reflection);
            MethodInfo method = type.GetMethod(methodName);
            Reflection c = new Reflection();
            string[] list = { arg1, arg2 };
            string result = (string)method.Invoke(c, list);
            return result;
        }



    }
}
