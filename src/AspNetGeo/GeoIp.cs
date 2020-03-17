namespace AspNetGeo
{
  using System.Collections.Generic;

  internal class GeoIp : IGeoIp
  {
    public IReadOnlyDictionary<string, string> CityNames { get; set; }

    public string ContinentCode { get; set; }

    public IReadOnlyDictionary<string, string> ContinentNames { get; set; }

    public string CountryCode { get; set; }

    public IReadOnlyDictionary<string, string> CountryNames { get; set; }

    public GeoLocation Location { get; set; }

    public string PostalCode { get; set; }

    public string RegionCode { get; set; }

    public IReadOnlyDictionary<string, string> RegionNames { get; set; }
  }
}