namespace AspNetGeo.Cache.InMemory
{
  using Microsoft.Extensions.DependencyInjection;

  public static class InMemoryCacheExtensions
  {
    public static IServiceCollection AddGeoIpInMemoryCache(this IServiceCollection services, int? maxSize = null)
    {
      services.AddSingleton<IGeoIpCache>(p => new GeoIpInMemoryCache(maxSize));
      return services;
    }
  }
}