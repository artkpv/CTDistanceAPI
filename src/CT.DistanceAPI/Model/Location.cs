using System;
using System.Collections.Generic;

namespace CT.DistanceAPI.Model
{
    // Or use System.Device.Location.GeoCoordinate
    public class Location
    {
        public Location() { }

        public static Location Empty { get; private set; } = new Location() {
            Lon = double.PositiveInfinity,
            Lat = double.PositiveInfinity 
        } ;

        public double Lon { get; set; }

        public double Lat { get; set; }

        public static Location FromLatLong(double lat, double lon)
            => new Location { Lat = lat, Lon = lon };

        public override string ToString() => $"<Location Lat={Lat} Lon={Lon}>";

        #region Equals
        public override bool Equals(object obj) => Equals(obj as Location);

        public bool Equals(Location other)  // IEquatable<Location>
        {
            if (object.ReferenceEquals(other, null))
                return false;
            if (object.ReferenceEquals(this, other))
                return true;
            if (this.GetType() != other.GetType())
                return false;
            return Lon == other.Lon && Lat == other.Lat;
        }

        public override int GetHashCode() => base.GetHashCode();

        public static bool operator ==(Location x, Location y) =>
            (object.ReferenceEquals(x, null) && object.ReferenceEquals(y, null))
            || (!object.ReferenceEquals(x, null) && x.Equals(y));

        public static bool operator !=(Location x, Location y) => !(x == y);
#endregion
    }
}
