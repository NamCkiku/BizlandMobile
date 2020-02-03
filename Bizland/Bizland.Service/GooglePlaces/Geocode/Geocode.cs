using Newtonsoft.Json;

using System.Collections.Generic;

namespace Bizland.Service
{
    public class Geocode
    {
        [JsonProperty("results")]
        public List<GeocodeResult> results { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }
    }
}