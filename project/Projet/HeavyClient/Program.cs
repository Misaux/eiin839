using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeavyClient.RoutingWithBikesLibrary;

namespace HeavyClient
{
    class Program
    {
        private static IRoutingWithBikes routingWithBikes = new RoutingWithBikesClient();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to RoutingWithBikes CLI");
            Console.WriteLine();

            while (true)
            {
                Console.Write("CLI> ");

                string input = Console.ReadLine();
                string[] arguments = input.Split(' ');
                string[] res;

                Console.WriteLine();

                switch (arguments[0])
                {
                    case ("help"):
                        Console.WriteLine("direction: direction between 2 positions without jcdecaux \"direction start_lat start_lon end_lat end_long\"");
                        Console.WriteLine("demo: \"classic demo\"");
                        Console.WriteLine("path: path between 2 positions with jcdecaux \"path start_lat start_lon end_lat end_long\"");

                        break;

                    case ("direction"):
                        res = routingWithBikes.GetPathDirections(Convert.ToSingle(arguments[1]), Convert.ToSingle(arguments[2]), Convert.ToSingle(arguments[3]), Convert.ToSingle(arguments[4]));

                        foreach (string s in res)
                        {
                            Console.WriteLine(s);
                        }
                        break;

                    case ("path"):
                        path(Convert.ToSingle(arguments[1], new CultureInfo("en-US")), Convert.ToSingle(arguments[2], new CultureInfo("en-US")), Convert.ToSingle(arguments[3], new CultureInfo("en-US")), Convert.ToSingle(arguments[4], new CultureInfo("en-US")));

                        break;

                    case ("demo"):
                        path(43.273886f, 5.383903f, 43.325274f, 5.366893f);

                        break;

                    default:
                        Console.WriteLine("error, see help to see options");
                        break;
                }
                Console.WriteLine();

            }
        }


        static void path(float s1, float s2, float e1, float e2)
        {
            string[][] res = routingWithBikes.GetFullDirections(s1, s2, e1, e2);

            Console.WriteLine("To Station: ");
            foreach (string s in res[0])
            {
                Console.WriteLine(s);
            }

            Console.WriteLine();

            Console.WriteLine("With bike: ");
            foreach (string s in res[1])
            {
                Console.WriteLine(s);
            }

            Console.WriteLine();

            Console.WriteLine("To Destination: ");
            foreach (string s in res[2])
            {
                Console.WriteLine(s);
            }
        }
    }
}
