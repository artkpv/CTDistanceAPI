using System;
using System.Threading.Tasks;
using CT.DistanceAPI.Model;
using Xunit;

namespace CT.DistanceAPI.Tests
{
    public class AirportsAPIAdapterTests
    {
        [Fact]
        public async Task Gets()
        {
            IAirportsAPIAdapter adapter = new AirportsAPIAdapter();
            
            // Act 
            Location location = await adapter.GetLocation(IATACode.FromString("AMS"));

            // Assert
            Assert.Equal(
                Location.FromLatLong(52.309069, 4.763385),
                location
            );
        }

        [Fact]
        public async Task GetsInvalid()
        {
            IAirportsAPIAdapter adapter = new AirportsAPIAdapter();
            
            // Act 
            Location location = await adapter.GetLocation(IATACode.FromString("INVAlid"));

            // Assert
            Assert.Equal(Location.Empty, location);
        }
        
        [Fact]
        public async Task ThrowsOnInvalidCode()
        {
            // Arrange // Act // Assert
            await Assert.ThrowsAsync<ApplicationException>(async () =>
                await new AirportsAPIAdapter().GetLocation(IATACode.FromString("")));
        }
    }
}
