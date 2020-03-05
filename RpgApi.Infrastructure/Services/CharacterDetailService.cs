using System;
using Dapper;
using RpgApi.Domain.Models;
using RpgApi.Infrastructure.Interfaces;

namespace RpgApi.Infrastructure.Services
{
    public sealed class CharacterDetailService : IReadUpdateService<CharacterDetail, int>
    {
        private readonly IConnection _connection;

        public CharacterDetailService(IConnection connection)
        {
            _connection = connection;
        }

        public CharacterDetail GetByCharacterId(int id)
        {
            var query = "SELECT * FROM CharacterDetail WHERE CharacterId=@id";
            using var conn = _connection.GetDbConnection();
            return conn.QuerySingle<CharacterDetail>(query, new {id});
        }

        public void Update(int id, CharacterDetail item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var query = "UPDATE CharacterDetail SET Name=@Name, PlayerName=@PlayerName WHERE CharacterId=@id";
            var result = GetByCharacterId(id) ?? throw new Exception($"{id} Not found");
            using var conn = _connection.GetDbConnection();
            conn.Execute(query, new {id = result.Id, item.Name, item.PlayerName});
        }
    }
}