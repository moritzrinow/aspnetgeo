# aspnetgeo
Add geo information to the ASP.NET Core request pipeline

## Supported providers

- MaxMind

## Usage

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

#### Access GeoIp data via HttpContext extension method (sits in HttpContext.Features)

```csharp
IGeoIp geoIP = context.GetGeoIp();
```

#### The GeoIp data structure

The name dictionaries are designed to be: locale ("en", "de" etc.) -> name.
The codes should come as ISO-2 codes.

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
