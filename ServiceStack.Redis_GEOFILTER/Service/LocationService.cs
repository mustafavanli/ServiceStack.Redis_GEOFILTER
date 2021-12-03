using ServiceStack.Redis;
using ServiceStack.Redis_GEOFILTER.Models;

namespace ServiceStack.Redis_GEOFILTER.Service
{
    public class LocationService
    {
        IRedisClientsManager _client;
        string LocationDateKey = "LocationDate:";
      
        public LocationService(IRedisClientsManager client)
        {
            _client = client;
        }

        public void LocationAdd(Location location)
        {
            using (var redis = _client.GetClient())
            {
                var geo = new RedisGeo();
                
                geo.Member = location.Name;
                geo.Latitude = location.Latitude;
                geo.Longitude = location.Longitude;
                var date = new LocationDate()
                {
                    EndDate = DateTime.Now,
                    Name = location.Name
                };

                redis.Set<LocationDate>(LocationDateKey + location.Name,date);               
                redis.AddGeoMembers(location.Country.ToUpper(), geo);
            }
        }

        public object Any(FindGeo request)
        {
            using (var redis = _client.GetClient())
            {
                List<GeoResult> ResultList = new List<GeoResult>();
                
                var RedisGeoResult = redis.FindGeoResultsInRadius(
                    request.Country.ToUpper(),
                    request.Lng,
                    request.Lat,
                    request.WithinKm,
                    RedisGeoUnit.Kilometers);

                foreach(var item in RedisGeoResult)
                {   
                    // GetInfo
                    LocationDate date = _client.GetClient().Get<LocationDate>(LocationDateKey + item.Member);

                    ResultList.Add(new GeoResult { 
                        Distance = item.Distance,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude, 
                        Hash = item.Hash, 
                        Member = item.Member, 
                        Unit = item.Unit,
                        // Additional info added to class
                        EndUpdate = date.EndDate.ToString()});

                }
                return ResultList;
            }
        }
    }
}
