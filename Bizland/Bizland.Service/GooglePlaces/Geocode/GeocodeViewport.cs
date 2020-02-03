using Newtonsoft.Json;

namespace Bizland.Service
{
    public class GeocodeViewport
    {
        [JsonProperty("northeast")]
        public GeocodeNortheast northeast { get; set; }

        [JsonProperty("southwest")]
        public GeocodeSouthwest southwest { get; set; }
    }
}