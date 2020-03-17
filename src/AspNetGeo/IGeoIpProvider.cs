namespace AspNetGeo
{
  using System;
  using System.Threading.Tasks;

  public interface IGeoIpProvider : IDisposable
  {
    IGeoIp Resolve(string address);

    Task<IGeoIp> ResolveAsync(string address);
  }
}