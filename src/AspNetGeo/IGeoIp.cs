namespace AspNetGeo
{
  using System.Collections.Generic;

  public interface IGeoIp
  {
    IReadOnlyDictionary<string, string> CityNames { get; }

    string ContinentCode { get; }

    IReadOnlyDictionary<string, string> ContinentNames { get; }

    string CountryCode { get; }

    IReadOnlyDictionary<string, string> CountryNames { get; }

    GeoLocation Location { get; }

    string PostalCode { get; }

    string RegionCode { get; }

    IReadOnlyDictionary<string, string> RegionNames { get; }
  }
}