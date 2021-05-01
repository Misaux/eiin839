using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Device.Location;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Globalization;
using System.ServiceModel.Web;
using RoutingWithBikesLibrary.WebProxyService;

namespace RoutingWithBikesLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class RoutingWithBikes : IRoutingWithBikes
    {
        IWebProxyService WebProxyService = new WebProxyServiceClient();

        public RoutingWithBikes()
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public List<Station> GetAllStations()
        {
            return new List<Station>(WebProxyService.GetAllStations());
        }

        public List<Station> GetNearestStation(float latitude, float longitude)
        {
            GeoCoordinate geoCoordinate = new GeoCoordinate(latitude, longitude);
            List<Station> stations = GetAllStations();

            stations.Sort((station1, station2) =>
            {
                GeoCoordinate g1 = new GeoCoordinate(station1.position.latitude, station1.position.longitude);
                GeoCoordinate g2 = new GeoCoordinate(station2.position.latitude, station2.position.longitude);
                return g1.GetDistanceTo(geoCoordinate).CompareTo(g2.GetDistanceTo(geoCoordinate));
            });

            return stations;
        }

        public Station GetNearestStationWithBikes(float latitude, float longitude)
        {
            GeoCoordinate geoCoordinate = new GeoCoordinate(latitude, longitude);
            List<Station> stations = GetAllStations();

            stations.Sort((station1, station2) =>
            {
                GeoCoordinate g1 = new GeoCoordinate(station1.position.latitude, station1.position.longitude);
                GeoCoordinate g2 = new GeoCoordinate(station2.position.latitude, station2.position.longitude);
                return g1.GetDistanceTo(geoCoordinate).CompareTo(g2.GetDistanceTo(geoCoordinate));
            });
            
            while(WebProxyService.GetStation(stations[0].number.ToString(), stations[0].contractName).totalStands.availabilities.bikes <= 0)
            {
                stations.RemoveAt(0);
            }

            return stations[0];
        }

        public Station GetNearestStationWithStands(float latitude, float longitude)
        {
            GeoCoordinate geoCoordinate = new GeoCoordinate(latitude, longitude);
            List<Station> stations = GetAllStations();

            stations.Sort((station1, station2) =>
            {
                GeoCoordinate g1 = new GeoCoordinate(station1.position.latitude, station1.position.longitude);
                GeoCoordinate g2 = new GeoCoordinate(station2.position.latitude, station2.position.longitude);
                return g1.GetDistanceTo(geoCoordinate).CompareTo(g2.GetDistanceTo(geoCoordinate));
            });

            while (WebProxyService.GetStation(stations[0].number.ToString(), stations[0].contractName).totalStands.availabilities.stands <= 0)
            {
                stations.RemoveAt(0);
            }

            return stations[0];
        }

        public List<Position> GetPath(float Slatitude, float Slongitude, float Elatitude, float Elongitude)
        {
            string URL = "https://api.openrouteservice.org/v2/directions/driving-car?api_key=5b3ce3597851110001cf6248bfc84e7836014868bc4ba08937e5efd0&start=" + Slongitude.ToString("G", CultureInfo.InvariantCulture) + "," + Slatitude.ToString("G", CultureInfo.InvariantCulture) + "&end=" + Elongitude.ToString("G", CultureInfo.InvariantCulture) + "," + Elatitude.ToString("G", CultureInfo.InvariantCulture);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "GET";
            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();
            var responseReader = new StreamReader(webStream);
            string response = responseReader.ReadToEnd();
            responseReader.Close();

            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            dynamic dobj = jsonSerializer.Deserialize<dynamic>(response);

            List<object> res = new List<object>(dobj["features"][0]["geometry"]["coordinates"]);

            List<Position> answer = new List<Position>();

            foreach(object[] obj in res)
            {
                answer.Add(new Position() { latitude = Convert.ToSingle(obj[0]), longitude = Convert.ToSingle(obj[1]) });
            }

            return answer;
        }

        public List<string> GetPathDirections(float Slatitude, float Slongitude, float Elatitude, float Elongitude)
        {
            string URL = "https://api.openrouteservice.org/v2/directions/driving-car?api_key=5b3ce3597851110001cf6248bfc84e7836014868bc4ba08937e5efd0&start=" + Slongitude.ToString("G", CultureInfo.InvariantCulture) + "," + Slatitude.ToString("G", CultureInfo.InvariantCulture) + "&end=" + Elongitude.ToString("G", CultureInfo.InvariantCulture) + "," + Elatitude.ToString("G", CultureInfo.InvariantCulture);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "GET";
            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();
            var responseReader = new StreamReader(webStream);
            string response = responseReader.ReadToEnd();
            responseReader.Close();

            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            dynamic dobj = jsonSerializer.Deserialize<dynamic>(response);

            List<object> res = new List<object>(dobj["features"][0]["properties"]["segments"][0]["steps"]);

            List<string> answer = new List<string>();

            foreach (Dictionary<string, object> obj in res)
            {
                answer.Add(obj["instruction"].ToString() + " for " + obj["distance"] + "m" );
            }

            return answer;
        }

        public List<List<Position>> GetFullPath(float Slatitude, float Slongitude, float Elatitude, float Elongitude)
        {
            Station startingStation = GetNearestStationWithBikes(Slatitude, Slongitude);
            Station endingStation = GetNearestStationWithStands(Elatitude, Elongitude);

            List<List<Position>> answer = new List<List<Position>>();

            answer.Add(GetPath(Slatitude, Slongitude, startingStation.position.latitude, startingStation.position.longitude));
            answer.Add(GetPath(startingStation.position.latitude, startingStation.position.longitude, endingStation.position.latitude, endingStation.position.longitude));
            answer.Add(GetPath(endingStation.position.latitude, endingStation.position.longitude, Elatitude, Elongitude));

            return answer;
        }

        public List<List<string>> GetFullDirections(float Slatitude, float Slongitude, float Elatitude, float Elongitude)
        {
            Station startingStation = GetNearestStationWithBikes(Slatitude, Slongitude);
            Station endingStation = GetNearestStationWithStands(Elatitude, Elongitude);

            List<List<string>> answer = new List<List<string>>();

            answer.Add(GetPathDirections(Slatitude, Slongitude, startingStation.position.latitude, startingStation.position.longitude));
            answer.Add(GetPathDirections(startingStation.position.latitude, startingStation.position.longitude, endingStation.position.latitude, endingStation.position.longitude));
            answer.Add(GetPathDirections(endingStation.position.latitude, endingStation.position.longitude, Elatitude, Elongitude));

            return answer;
        }
    }
}
