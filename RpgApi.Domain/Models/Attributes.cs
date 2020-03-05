using System;
using System.Text.Json.Serialization;

namespace RpgApi.Domain.Models
{
    public class Attributes
    {
        [JsonIgnore] public Guid Id { get; set; }

        [JsonPropertyName("strength")] public int Strength { get; set; }

        [JsonPropertyName("dexterity")] public int Dexterity { get; set; }

        [JsonPropertyName("constitution")] public int Constitution { get; set; }

        [JsonPropertyName("intelligence")] public int Intelligence { get; set; }

        [JsonPropertyName("wisdom")] public int Wisdom { get; set; }

        [JsonPropertyName("charisma")] public int Charisma { get; set; }
    }
}