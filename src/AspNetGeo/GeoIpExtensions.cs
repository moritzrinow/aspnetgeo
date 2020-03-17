namespace AspNetGeo
{
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Http;

  public static class GeoIpExtensions
  {
    public static IApplicationBuilder UseGeoIp(this IApplicationBuilder app)
    {
      app.UseMiddleware<GeoIpMiddleware>();
      return app;
    }

    public static IGeoIp GetGeoIp(this HttpContext context)
    {
      context.Items.TryGetValue("geoIp", out object val);
      return val as IGeoIp;
    }

    public static string GetCountryCode(this HttpContext context)
    {
      var geoIp = context.GetGeoIp();
      return geoIp?.CountryCode;
    }

    public static string GetContinentCode(this HttpContext context)
    {
      var geoIp = context.GetGeoIp();
      return geoIp?.ContinentCode;
    }

    public static string GetPostalCode(this HttpContext context)
    {
      var geoIp = context.GetGeoIp();
      return geoIp?.PostalCode;
    }

    public static string GetRegionCode(this HttpContext context)
    {
      var geoIp = context.GetGeoIp();
      return geoIp?.RegionCode;
    }

    public static GeoLocation GetGeoLocation(this HttpContext context)
    {
      var geoIp = context.GetGeoIp();
      return geoIp?.Location;
    }
  }
}