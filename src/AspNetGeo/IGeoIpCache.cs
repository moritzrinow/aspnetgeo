namespace AspNetGeo
{
  public interface IGeoIpCache
  {
    IGeoIp Get(string address);

    void Put(string address, IGeoIp geoIp);
  }
}