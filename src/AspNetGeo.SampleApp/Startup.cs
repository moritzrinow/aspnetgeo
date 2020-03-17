namespace AspNetGeo.SampleApp
{
  using System;
  using System.IO;
  using System.Net;
  using AspNetGeo.Cache.InMemory;
  using AspNetGeo.Provider.MaxMind;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.Http;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;

  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.UseMaxMindGeoIp(Path.Combine(Directory.GetCurrentDirectory(), "data"));
      services.AddGeoIpInMemoryCache();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.Use(async (context, next) =>
      {
        context.Connection.RemoteIpAddress = this.GenerateIpv4();
        await next();
      });

      app.UseGeoIp();

      app.UseRouting();

      app.UseEndpoints(endpoints => { endpoints.MapGet("/", async context =>
      {
        IGeoIp geoIP = context.GetGeoIp();
        await context.Response
                     .WriteAsync($"Location: [{geoIP?.Location?.Latitude} " + 
                                 $"| {geoIP?.Location?.Longitude}] " +
                                 $"Continent: [{context.GetContinentCode()}] " + 
                                 $"Country: [{context.GetCountryCode()}]");
      }); });
    }

    private IPAddress GenerateIpv4()
    {
      byte[] data = new byte[4];
      Random random = new Random();
      random.NextBytes(data);

      while ((data[0] | 0) == 0)
      {
        random.NextBytes(data);
      }

      return new IPAddress(data);
    }
  }
}