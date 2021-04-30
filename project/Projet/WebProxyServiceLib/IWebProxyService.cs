using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WebProxy
{
    [ServiceContract]
    public interface IWebProxyService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "AllStations", Method = "GET", RequestFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Station> GetAllStations();

        [OperationContract]
        [WebInvoke(UriTemplate = "AllStations?stationId={stationId}&stationContract{stationContract}", Method = "GET", RequestFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Station GetStation(string stationId, string stationContract);
    }
}
