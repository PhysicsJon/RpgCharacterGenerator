using Dapper;
using RpgApi.Domain.Models;
using RpgApi.Infrastructure.Interfaces;
using System;
using System.Threading.Tasks;

namespace RpgApi.Infrastructure.Services
{
    public sealed class CharacterDetailServiceAsync : IReadUpdateServiceAsync<CharacterDetail, int>
    {
        private readonly IConnection _connection;

        public CharacterDetailServiceAsync(IConnection connection)
        {
            _connection = connection;
        }

        public async Task<CharacterDetail> GetByCharacterIdAsync(int id)
        {
            var query = "SELECT * FROM CharacterDetail WHERE CharacterId=@id";
            using var conn = _connection.GetDbConnection();
            return await conn.QuerySingleAsync<CharacterDetail>(query, new { id }).ConfigureAwait(false);
        }

        public async Task UpdateAsync(int id, CharacterDetail item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var query = "UPDATE CharacterDetail SET Name=@Name, PlayerName=@PlayerName WHERE CharacterId=@id";
            var result = GetByCharacterIdAsync(id) ?? throw new Exception($"{id} Not found");
            using var conn = _connection.GetDbConnection();
            await conn.ExecuteAsync(query, new { id = result.Id, item.Name, item.PlayerName }).ConfigureAwait(false);
        }
    }
}