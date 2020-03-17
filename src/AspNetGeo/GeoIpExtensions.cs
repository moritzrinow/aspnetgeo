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

    public static string GetCountryCode(this HttpContext context)
    {
      IGeoIp geoIp = context.Features.Get<IGeoIp>();
      return geoIp?.CountryCode;
    }

    public static string GetContinentCode(this HttpContext context)
    {
      IGeoIp geoIp = context.Features.Get<IGeoIp>();
      return geoIp?.ContinentCode;
    }

    public static string GetPostalCode(this HttpContext context)
    {
      IGeoIp geoIp = context.Features.Get<IGeoIp>();
      return geoIp?.PostalCode;
    }

    public static string GetRegionCode(this HttpContext context)
    {
      IGeoIp geoIp = context.Features.Get<IGeoIp>();
      return geoIp?.RegionCode;
    }

    public static GeoLocation GetGeoLocation(this HttpContext context)
    {
      IGeoIp geoIp = context.Features.Get<IGeoIp>();
      return geoIp?.Location;
    }
  }
}