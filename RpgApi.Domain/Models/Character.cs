using System.Text.Json.Serialization;

namespace RpgApi.Domain.Models
{
    public sealed class Character : EntityBase<int>
    {
        [JsonPropertyName("characterDetail")] public CharacterDetail CharacterDetail { get; set; }

        [JsonPropertyName("attributes")] public Attributes Attributes { get; set; }
    }
}