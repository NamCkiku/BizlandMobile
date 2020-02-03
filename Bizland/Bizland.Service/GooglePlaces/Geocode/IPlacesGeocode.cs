using System.Threading.Tasks;

namespace Bizland.Service
{
    public interface IPlacesGeocode
    {
        Task<Geocode> GetGeocode(string input);

        Task<Geocode> GetAddressesForPosition(string lat, string lng);
    }
}