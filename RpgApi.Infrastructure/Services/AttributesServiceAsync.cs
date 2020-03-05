using System;
using System.Threading.Tasks;
using Dapper;
using RpgApi.Domain.Models;
using RpgApi.Infrastructure.Interfaces;

namespace RpgApi.Infrastructure.Services
{
    public sealed class AttributesServiceAsync : IReadUpdateServiceAsync<Attributes, int>
    {
        private readonly IConnection _connection;

        public AttributesServiceAsync(IConnection connection)
        {
            _connection = connection;
        }

        public async Task<Attributes> GetByCharacterIdAsync(int id)
        {
            var query = "SELECT * FROM Attributes WHERE CharacterId=@id";
            using var conn = _connection.GetDbConnection();
            return await conn.QuerySingleAsync<Attributes>(query, new {id}).ConfigureAwait(false);
        }

        public async Task UpdateAsync(int id, Attributes item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var character = GetByCharacterIdAsync(id) ?? throw new Exception($"{id} Not Found");
            var query =
                "UPDATE Attributes SET Strength=@Strength, Dexterity=@Dexterity, Constitution=@Constitution, Intelligence=@Intelligence, Wisdom=@Wisdom, Charisma=@Charisma WHERE CharacterId=@id";
            using var conn = _connection.GetDbConnection();
            await conn.ExecuteAsync(query,
                new
                {
                    id = character.Id, item.Strength, item.Dexterity, item.Constitution, item.Charisma,
                    item.Intelligence, item.Wisdom
                }).ConfigureAwait(false);
        }
    }
}