namespace AspNetGeo
{
  using System;
  using System.Net;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Http;

  public class GeoIpMiddleware
  {
    private IGeoIpCache cache;
    private IGeoIpProvider provider;
    private readonly RequestDelegate next;

    public GeoIpMiddleware(RequestDelegate next, IGeoIpProvider provider, IGeoIpCache cache)
    {
      this.next = next;

      this.provider = provider ?? throw new Exception("No geoIp provider was injected");
      this.cache = cache;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      string address = context.Connection.RemoteIpAddress.ToString();
      IGeoIp geoIp = null;

      if (this.cache != null)
      {
        geoIp = this.cache.Get(address);

        if (geoIp == null)
        {
          geoIp = this.provider.Resolve(address);
          this.cache.Put(address, geoIp);
        }
      }
      else
      {
        geoIp = this.provider.Resolve(address);
      }

      context.Features.Set(geoIp);

      await this.next(context);
    }
  }
}