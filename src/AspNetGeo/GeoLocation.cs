namespace AspNetGeo
{
  public class GeoLocation
  {
    public GeoLocation(double? latitude, double? longitude)
    {
      this.Latitude = latitude;
      this.Longitude = longitude;
    }

    public double? Latitude { get; }

    public double? Longitude { get; }
  }
}