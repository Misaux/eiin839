using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Device.Location;
using Newtonsoft.Json;

namespace WebProxy
{
    public class JCDecauxItem
    {
        static HttpClient httpClient = new HttpClient();
        static string apiKey = "64346e2dbae9c6364bd75b2ee766c8e33623b537";
        static string apiKeyEntry = "&apiKey=" + apiKey;
        static Task<string> response;

        public static void getContracts()
        {
            response = httpClient.GetStringAsync("https://api.jcdecaux.com/vls/v1/contracts?apiKey=" + apiKey);
            response.Wait();
            Console.WriteLine(response.Status);
            Contract[] stations = JsonConvert.DeserializeObject<Contract[]>(response.Result);
            foreach (Contract item in stations)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public static List<Station> getStationsFromContract(string contract)
        {
            response = httpClient.GetStringAsync("https://api.jcdecaux.com/vls/v1/stations?contract=" + contract + "&apiKey=" + apiKey);
            response.Wait();
            return JsonConvert.DeserializeObject<List<Station>>(response.Result);
        }

        public static List<Station> getAllStations()
        {
            response = httpClient.GetStringAsync("https://api.jcdecaux.com/vls/v3/stations?" + apiKeyEntry);
            response.Wait();
            return JsonConvert.DeserializeObject<List<Station>>(response.Result);
        }

        public static Station getStationInfos(string stationId, string contract)
        {
            response = httpClient.GetStringAsync("https://api.jcdecaux.com/vls/v3/stations/" + stationId + "?contract=" + contract + "&apiKey=" + apiKey);
            response.Wait();
            return JsonConvert.DeserializeObject<Station>(response.Result);
        }

        public static Station getNearestStation(string contract, GeoCoordinate geoCoordinate)
        {
            List<Station> stations = getStationsFromContract(contract);

            stations.Sort((station1, station2) =>
            {
                GeoCoordinate g1 = new GeoCoordinate(station1.position.latitude, station1.position.longitude);
                GeoCoordinate g2 = new GeoCoordinate(station2.position.latitude, station2.position.longitude);
                return g1.GetDistanceTo(geoCoordinate).CompareTo(g2.GetDistanceTo(geoCoordinate));
            });

            return stations[0];
        }
    }

    public class Station
    {
        public int number { get; set; }
        public string contractName { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string status { get; set; }
        public TotalStands totalStands { get; set; }
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
        public float latitude { get; set; }
        public float longitude { get; set; }
    }

    public class TotalStands
    {
        public Availabilities availabilities { get; set; }
    }

    public class Availabilities
    {
        public int stands { get; set; }

        public int bikes { get; set; }
    }
}
