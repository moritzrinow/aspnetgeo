namespace AspNetGeo.Provider.MaxMind
{
  using Microsoft.Extensions.DependencyInjection;

  public static class MaxMindProviderExtensions
  {
    public static IServiceCollection UseMaxMindGeoIp(this IServiceCollection services, string dir)
    {
      services.AddSingleton<IGeoIpProvider>(p => new MaxMindGeoIpProvider(dir));
      return services;
    }
  }
}