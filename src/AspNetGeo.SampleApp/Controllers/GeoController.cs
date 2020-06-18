namespace AspNetGeo.SampleApp.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  [ApiController]
  [Route("geo")]
  public class GeoController : ControllerBase
  {
    /// <summary>
    /// Gets this instance.
    /// </summary>
    /// <returns></returns>
    public IActionResult Get()
    {
      var geoIp = this.HttpContext.GetGeoIp();
      return this.Ok(geoIp);
    }
  }
}