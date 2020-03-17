namespace AspNetGeo.Provider.MaxMind
{
  using System;
  using System.IO;
  using System.Linq;
  using System.Threading.Tasks;
  using global::MaxMind.GeoIP2;

  public class MaxMindGeoIpProvider : IGeoIpProvider
  {
    public const string MAXMIND_DB_FILE_ENDING = "mmdb";
    public const string MAXMIND_CITY_DB_FILE_PATTERN = "*-City." + MaxMindGeoIpProvider.MAXMIND_DB_FILE_ENDING;
    public const string MAXMIND_COUNTRY_DB_FILE_PATTERN = "*-Country." + MaxMindGeoIpProvider.MAXMIND_DB_FILE_ENDING;

    private bool hasCity;
    private DatabaseReader reader;

    public MaxMindGeoIpProvider(string path)
    {
      var info = new DirectoryInfo(path);

      if (!info.Exists)
      {
        throw new Exception("MaxMind data directory does not exist");
      }

      var cityFiles = info.GetFiles(MaxMindGeoIpProvider.MAXMIND_CITY_DB_FILE_PATTERN);
      var countryFiles = info.GetFiles(MaxMindGeoIpProvider.MAXMIND_COUNTRY_DB_FILE_PATTERN);

      if (cityFiles.Any())
      {
        string fileName = cityFiles.First().FullName;
        this.reader = new DatabaseReader(fileName);
        this.hasCity = true;
      }
      else if (countryFiles.Any())
      {
        string fileName = countryFiles.First().FullName;
        this.reader = new DatabaseReader(fileName);
      }
      else
      {
        throw new Exception("Data directory does not contain data files");
      }
    }

    public IGeoIp Resolve(string address)
    {
      var geoIp = new GeoIp();

      if (this.hasCity)
      {
        bool found = this.reader.TryCity(address, out var city);
        if (!found)
        {
          return null;
        }

        geoIp.Location = new GeoLocation(city.Location.Latitude, city.Location.Longitude);
        geoIp.ContinentCode = city.Continent?.Code;
        geoIp.CountryCode = city.Country?.IsoCode;
        geoIp.RegionCode = city.MostSpecificSubdivision?.IsoCode;
        geoIp.PostalCode = city.Postal?.Code;

        geoIp.ContinentNames = city.Continent?.Names;
        geoIp.CountryNames = city.Country?.Names;
        geoIp.RegionNames = city.MostSpecificSubdivision?.Names;
        geoIp.CityNames = city.City?.Names;

        return geoIp;
      }
      else
      {
        bool found = this.reader.TryCountry(address, out var country);
        if (!found)
        {
          return null;
        }

        geoIp.ContinentCode = country.Continent?.Code;
        geoIp.CountryCode = country.Country?.IsoCode;

        geoIp.ContinentNames = country.Continent?.Names;
        geoIp.CountryNames = country.Country?.Names;

        return geoIp;
      }
    }

    public async Task<IGeoIp> ResolveAsync(string address)
    {
      return this.Resolve(address);
    }

    public void Dispose()
    {
      this.reader?.Dispose();
    }
  }
}