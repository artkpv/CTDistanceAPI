

using System;
using System.Net;
using System.Threading.Tasks;
using RestSharp;

namespace CT.DistanceAPI.Model
{
    public class AirportsAPIAdapter : IAirportsAPIAdapter
    {
        private RestClient client 
            = new RestClient("https://places-dev.cteleport.com");

        public async Task<Location> GetLocation(IATACode code)
        {
            if (!code.IsValid)
                throw new ApplicationException("Need valid code");

            var request 
                = new RestRequest("airports/" + code.Code, DataFormat.Json);

            IRestResponse<AiportDTO> response 
                = await client.ExecuteAsync<AiportDTO>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException(
                    "Invalid API request",
                    response.ErrorException
                );
            }
            if (response.StatusCode != HttpStatusCode.OK)
                return Location.Empty;
            return response.Data.Location;
        }
    }
}

