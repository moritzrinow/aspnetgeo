namespace AspNetGeo.Cache.InMemory
{
  using System.Collections.Concurrent;

  internal class GeoIpInMemoryCache : IGeoIpCache
  {
    private int? maxSize;
    private ConcurrentDictionary<string, IGeoIp> cache;

    public GeoIpInMemoryCache(int? maxSize = null)
    {
      this.cache = new ConcurrentDictionary<string, IGeoIp>();
    }

    public IGeoIp Get(string address)
    {
      this.cache.TryGetValue(address, out IGeoIp geoIp);
      return geoIp;
    }

    public void Put(string address, IGeoIp geoIp)
    {
      if (this.cache.TryGetValue(address, out IGeoIp oldGeoIp))
      {
        this.cache.AddOrUpdate(address, geoIp, (p, q) => geoIp);
      }
      else
      {
        if (this.maxSize.HasValue && (this.cache.Count + 1) > this.maxSize)
        {
          this.cache.Clear();
        }

        this.cache.TryAdd(address, geoIp);
      }
    }
  }
}