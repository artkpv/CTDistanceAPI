using System;
using System.Device.Location;

namespace CT.DistanceAPI.Model
{
    public class DistanceService
    {
        private const double MetersInMile = 1609.344;
        public const double EpsilonDistance = 1E-3;

        public double CalcDistanceMiles(Location aL, Location bL)
        {
            if (aL == Location.Empty || bL == Location.Empty)
                throw new ApplicationException("Invalid locations");
            var aC = new GeoCoordinate(aL.Lat, aL.Lon);
            var bC = new GeoCoordinate(bL.Lat, bL.Lon);
            double meters = aC.GetDistanceTo(bC);
            return meters / MetersInMile;
        }

    }
}

