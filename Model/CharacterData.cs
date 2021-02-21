using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SWGOH.DATA.Model
{
    public class CharacterData
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("base_id")]
        public string BaseId { get; set; }

        [JsonPropertyName("pk")]
        public int Pk { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("power")]
        public int Power { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("combat_type")]
        public int CombatType { get; set; }

        [JsonPropertyName("gear_levels")]
        public List<GearLevel> GearLevels { get; set; }

        [JsonPropertyName("alignment")]
        public string Alignment { get; set; }

        [JsonPropertyName("categories")]
        public List<string> Categories { get; set; }

        [JsonPropertyName("ability_classes")]
        public List<string> AbilityClasses { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("ship")]
        public string Ship { get; set; }

        [JsonPropertyName("ship_slot")]
        public int? ShipSlot { get; set; }

        [JsonPropertyName("activate_shard_count")]
        public int ActivateShardCount { get; set; }
    }
}