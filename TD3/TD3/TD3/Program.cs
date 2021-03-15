using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Device.Location;
using System.Collections.Generic;

namespace TD3
{
    class Program
    {
        static HttpClient httpClient = new HttpClient();
        static string apiKey = "64346e2dbae9c6364bd75b2ee766c8e33623b537";
        static Task<string> response;

        static void getContracts()
        {
            response = httpClient.GetStringAsync("https://api.jcdecaux.com/vls/v1/contracts?apiKey=" + apiKey);
            response.Wait();
            Console.WriteLine(response.Status);
            Contract[] stations = JsonSerializer.Deserialize<Contract[]>(response.Result);
            foreach (Contract item in stations)
            {
                Console.WriteLine(item.ToString());
            }
        }

        static List<Station> getStationsFromContract(string contract)
        {
            response = httpClient.GetStringAsync("https://api.jcdecaux.com/vls/v1/stations?contract=" + contract + "&apiKey=" + apiKey);
            response.Wait();
            Console.WriteLine(response.Status);
            return JsonSerializer.Deserialize<List<Station>>(response.Result);
        }

        static void getStationInfos(string stationId, string contract)
        {
            response = httpClient.GetStringAsync("https://api.jcdecaux.com/vls/v3/stations/" + stationId + "?contract=" + contract + "&apiKey=" + apiKey);
            response.Wait();
            Console.WriteLine(response.Status);
            Console.WriteLine(response.Result);
        }

        static Station getNearestStation(string contract, GeoCoordinate geoCoordinate)
        {
            List<Station> stations = getStationsFromContract(contract);

            stations.Sort((station1, station2) =>
            {
                GeoCoordinate g1 = new GeoCoordinate(station1.position.lat, station1.position.lng);
                GeoCoordinate g2 = new GeoCoordinate(station2.position.lat, station2.position.lng);
                return g1.GetDistanceTo(geoCoordinate).CompareTo(g2.GetDistanceTo(geoCoordinate));
            });

            return stations[0];
        }

        static void Main(string[] args)
        {
            try
            {
                switch (args.Length)
                {
                    case 0:
                        getContracts();
                        break;

                    case 1:
                        List<Station> stationsFromContract = getStationsFromContract(args[0]);
                        foreach (Station item in stationsFromContract)
                        {
                            Console.WriteLine(item.ToString());
                        }
                        break;
                    case 2:
                        getStationInfos(args[0], args[1]);
                        break;

                    case 3:
                        GeoCoordinate target = new GeoCoordinate(Convert.ToDouble(args[0]), Convert.ToDouble(args[1]));

                        Console.WriteLine(getNearestStation(args[2], target).ToString());                        
                        break;
                }
                var name = Console.ReadLine();
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
        public Position position { get; set; }

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