using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CT.DistanceAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CT.DistanceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DistanceServiceController : ControllerBase
    {
        private readonly ILogger<DistanceServiceController> logger;
        private readonly DistanceService service;
        private readonly AirportsAPIAdapter airportsAPI;

        public DistanceServiceController(
            ILogger<DistanceServiceController> logger)
        {
            this.logger = logger;
            this.service = new DistanceService();
            this.airportsAPI = new AirportsAPIAdapter();
        }

        [HttpGet]
        [Route("{from}-{to}")]
        public async Task<ActionResult<string>> Get(string from, string to)
        {
            IATACode fromCode = IATACode.FromString(from);
            IATACode toCode = IATACode.FromString(to);

            if (!fromCode.IsValid || !toCode.IsValid)
                return BadRequest("Invalid codes");

            Task<Location> toRequest = airportsAPI.GetLocation(toCode);
            Task<Location> fromRequest = airportsAPI.GetLocation(fromCode);
            try 
            {
                await Task.WhenAll(toRequest, fromRequest);
            }
            catch (ApplicationException ex)
            {
                logger.LogError("Requests faulted.", ex);
                throw;
            }

            Location toLocation = toRequest.Result;
            Location fromLocation = fromRequest.Result;

            if (toLocation == Location.Empty || fromLocation == Location.Empty)
                return BadRequest("Invalid codes");
            
            double distanceMiles =
                service.CalcDistanceMiles(fromLocation, toLocation);
            return $"Distance between {fromCode} and {toCode}: {distanceMiles}";

        }
    }
}
