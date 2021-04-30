using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyClient
{
    class Program
    {
        private static RoutingWithBikes.IRoutingWithBikes routingWithBikes = new RoutingWithBikes.RoutingWithBikesClient();

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
                        path(Convert.ToSingle(arguments[1]), Convert.ToSingle(arguments[2]), Convert.ToSingle(arguments[3]), Convert.ToSingle(arguments[4]));

                        break;

                    case ("demo"):
                        path(43.135902f, 6.001995f, 43.137233f, 5.998016f);

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
