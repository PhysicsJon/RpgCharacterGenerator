using System;
using System.Text.Json.Serialization;

namespace RpgApi.Domain.Models
{
    public sealed class CharacterDetail
    {
        [JsonIgnore] public Guid Id { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("playerName")] public string PlayerName { get; set; }
    }
}