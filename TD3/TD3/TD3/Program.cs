using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TD3
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            string apiKey = "64346e2dbae9c6364bd75b2ee766c8e33623b537";
            Task<string> response;

            try
            {
                switch (args.Length)
                {
                    case 0:
                        response = httpClient.GetStringAsync("https://api.jcdecaux.com/vls/v1/contracts?apiKey=" + apiKey);
                        response.Wait();
                        Console.WriteLine(response.Status);
                        Contract[] stations = JsonSerializer.Deserialize<Contract[]>(response.Result);
                        foreach (Contract item in stations)
                        {
                            Console.WriteLine(item.ToString());
                        }
                        break;

                    case 1:
                        response = httpClient.GetStringAsync("https://api.jcdecaux.com/vls/v1/stations?contract=" + args[0] + "&apiKey=" + apiKey);
                        response.Wait();
                        Console.WriteLine(response.Status);
                        Station[] stationsFromContract = JsonSerializer.Deserialize<Station[]>(response.Result);
                        foreach (Station item in stationsFromContract)
                        {
                            Console.WriteLine(item.ToString());
                        }
                        break;
                    case 2:
                        response = httpClient.GetStringAsync("https://api.jcdecaux.com/vls/v3/stations/" + args[0] + "?contract=" + args[1] + "&apiKey=" + apiKey);
                        response.Wait();
                        Console.WriteLine(response.Status);
                        Console.WriteLine(response.Result);
                        break;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Error");
            }
        }
    }

    public class Station
    {
        public int number { get; set; }
        public string contract_name { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string status { get; set; }
        public int available_bike_stands { get; set; }
        public Position position{ get; set; }

        public override string ToString()
        {
            return "Name: " + name + "\n" + "Number: " + number + "\n";
        }
    }

    public class Contract
    {
        public string name { get; set; }
        public override string ToString()
        {
            return "Name: " + name + "\n";
        }
    }


    public class Position
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }
}
