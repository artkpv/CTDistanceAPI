using System;
using System.Threading.Tasks;

namespace CT.DistanceAPI.Model
{
    public interface IAirportsAPIAdapter
    {
        Task<Location> GetLocation(IATACode code);
    }
}
