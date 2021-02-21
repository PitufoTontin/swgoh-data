using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SWGOH.DATA.Model
{
    public class GearLevel
    {
        [JsonPropertyName("tier")]
        public int Tier { get; set; }

        [JsonPropertyName("gear")]
        public List<string> Gear { get; set; }
    }
}