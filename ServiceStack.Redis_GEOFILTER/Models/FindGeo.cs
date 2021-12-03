namespace ServiceStack.Redis_GEOFILTER.Models
{
    public class FindGeo
    {
        public string Country { get; set; }
        public double WithinKm { get; set; }
        public double Lng { get; set; }
        public double Lat { get; set; }
    }
}
