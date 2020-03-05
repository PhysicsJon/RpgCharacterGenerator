using System.Text.Json.Serialization;

namespace RpgApi.Domain
{
    public class EntityBase<T>
    {
        [JsonPropertyName("id")] public T Id { get; set; }
    }
}