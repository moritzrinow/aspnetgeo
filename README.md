# aspnetgeo
Add geo information to the ASP.NET Core request pipeline

## Supported providers

- MaxMind

## Usage
 
#### Install package
 
dotnet add package AspNetGeo

#### Download free MaxMind databases and group them in a folder

https://dev.maxmind.com/geoip/geoip2/geolite2/
 
#### Add GeoIpMiddleware to the pipeline
```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
  // ...
  
  app.UseGeoIp();
  
  // ...
}
```

#### Use MaxMind as GeoIpProvider
```csharp
public void ConfigureServices(IServiceCollection services)
{
  // ...
  
  // Use path to directory with MaxMind database files
  services.UseMaxMindGeoIp(Path.Combine(Directory.GetCurrentDirectory(), "data"));
  
  // ...
}
```

#### Include in-memory cache
```csharp
public void ConfigureServices(IServiceCollection services)
{
  // ...
  
  // Use path to directory with MaxMind database files
  services.UseMaxMindGeoIp(Path.Combine(Directory.GetCurrentDirectory(), "data"));
  services.AddGeoIpInMemoryCache();
  // ...
}
```

#### Access GeoIp data via HttpContext extension method (data sits in HttpContext.Items under key 'geoIp')

```csharp
IGeoIp geoIP = context.GetGeoIp();
```

#### The GeoIp data structure

The name dictionaries are designed to be: locale ("en", "de" etc.) -> name. 
The continent & country codes should come as ISO alpha-2.

```csharp
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

```
