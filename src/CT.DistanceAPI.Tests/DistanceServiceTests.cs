using System;
using CT.DistanceAPI.Model;
using Xunit;

namespace CT.DistanceAPI.Tests
{
    public class DistanceServiceTests
    {
        [Fact]
        public void CalcsDistance()
        {
            // Arrange
            DistanceService service = new DistanceService();

            // Act
            double miles = service.CalcDistanceMiles(
                Location.FromLatLong(52.309069, 4.763385), // AMS
                Location.FromLatLong(59.799847, 30.270505) // LED
            );

            // Assert
            Assert.True(
                Math.Abs(miles - 1103.166) <= DistanceService.EpsilonDistance);
            
        }
    }
}
