using RoutingWithBikesLibrary.WebProxyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RoutingWithBikesLibrary
{
    [ServiceContract]
    public interface IRoutingWithBikes
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "AllStations", Method = "GET", RequestFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Station> GetAllStations();

        [OperationContract]
        [WebInvoke(UriTemplate = "NearestStation?lat={latitude}&lng={longitude}", Method = "GET", RequestFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Station> GetNearestStation(float latitude, float longitude);

        [OperationContract]
        [WebInvoke(UriTemplate = "NearestStationWithBikes?lat={latitude}&lng={longitude}", Method = "GET", RequestFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Station GetNearestStationWithBikes(float latitude, float longitude);

        [OperationContract]
        [WebInvoke(UriTemplate = "NearestStationWithStands?lat={latitude}&lng={longitude}", Method = "GET", RequestFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Station GetNearestStationWithStands(float latitude, float longitude);

        [OperationContract]
        [WebInvoke(UriTemplate = "Path?start_lat={Slatitude}&start_lng={Slongitude}&end_lat={Elatitude}&end_lng={Elongitude}", Method = "GET", RequestFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Position> GetPath(float Slatitude, float Slongitude, float Elatitude, float Elongitude);

        [OperationContract]
        [WebInvoke(UriTemplate = "PathDirection?start_lat={Slatitude}&start_lng={Slongitude}&end_lat={Elatitude}&end_lng={Elongitude}", Method = "GET", RequestFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<string> GetPathDirections(float Slatitude, float Slongitude, float Elatitude, float Elongitude);

        [OperationContract]
        [WebInvoke(UriTemplate = "FullPath?start_lat={Slatitude}&start_lng={Slongitude}&end_lat={Elatitude}&end_lng={Elongitude}", Method = "GET", RequestFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<List<Position>> GetFullPath(float Slatitude, float Slongitude, float Elatitude, float Elongitude);

        [OperationContract]
        [WebInvoke(UriTemplate = "FullDirection?start_lat={Slatitude}&start_lng={Slongitude}&end_lat={Elatitude}&end_lng={Elongitude}", Method = "GET", RequestFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<List<string>> GetFullDirections(float Slatitude, float Slongitude, float Elatitude, float Elongitude);
    }
}
