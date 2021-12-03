using ServiceStack.Redis;
using ServiceStack.Redis_GEOFILTER.Models;

namespace ServiceStack.Redis_GEOFILTER.Service
{
    public class LocationService
    {
        IRedisClientsManager _client;
        public LocationService(IRedisClientsManager client)
        {
            _client = client;
        }

        public void LocationAdd(Location location)
        {
            using (var redis = _client.GetClient())
            {
                var results = new RedisGeo();
                results.Member = location.Name;
                results.Latitude = location.Latitude;
                results.Longitude = location.Longitude;
                redis.AddGeoMembers(location.Country.ToUpper(), results);
            }
        }
        public object Any(FindGeo request)
        {
            var results = _client.GetClient().FindGeoResultsInRadius(
                request.Country.ToUpper(),
                request.Lng,
                request.Lat,
                request.WithinKm,
                RedisGeoUnit.Kilometers);
            return results;
        }


    }
}
