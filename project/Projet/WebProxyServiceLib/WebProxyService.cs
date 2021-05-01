using System;
using System.Runtime.Caching;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WebProxy
{
    public class WebProxyService : IWebProxyService
    {
        private ObjectCache cache = MemoryCache.Default;
        private CacheItemPolicy defaultCacheItemPolicy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1),
        };

        public WebProxyService()
        {

        }

        public List<Station> GetAllStations()
        {
            if (cache.Get("Stations") == null)
            {
                cache.Add("Stations", JCDecauxItem.getAllStations(), ObjectCache.InfiniteAbsoluteExpiration);
            }
            return (List<Station>)cache.Get("Stations");
        }

        public Station GetStation(string stationId, string stationContract)
        {
            if (cache.Get(stationId + stationContract) == null)
            {
                cache.Add(stationId + stationContract, JCDecauxItem.getStationInfos(stationId, stationContract), defaultCacheItemPolicy);
            }

            return (Station)cache.Get(stationId + stationContract);
        }
    }
}
