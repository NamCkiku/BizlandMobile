using System.Threading.Tasks;

namespace Bizland.Service
{
    public interface IPlacesAutocomplete
    {
        Task<Predictions> GetAutocomplete(string input);
    }
}