using System;
using Dapper;
using RpgApi.Domain.Models;
using RpgApi.Infrastructure.Interfaces;

namespace RpgApi.Infrastructure.Services
{
    public sealed class AttributesService : IReadUpdateService<Attributes, int>
    {
        private readonly IConnection _connection;

        public AttributesService(IConnection connection)
        {
            _connection = connection;
        }

        public Attributes GetByCharacterId(int id)
        {
            var query = "SELECT * FROM Attributes WHERE CharacterId=@id";
            using var conn = _connection.GetDbConnection();
            return conn.QuerySingle<Attributes>(query, new {id});
        }

        public void Update(int id, Attributes item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var character = GetByCharacterId(id) ?? throw new Exception($"{id} Not Found");
            var query =
                "UPDATE Attributes SET Strength=@Strength, Dexterity=@Dexterity, Constitution=@Constitution, Intelligence=@Intelligence, Wisdom=@Wisdom, Charisma=@Charisma WHERE CharacterId=@id";
            using var conn = _connection.GetDbConnection();
            conn.Execute(query,
                new
                {
                    id = character.Id, item.Strength, item.Dexterity, item.Constitution, item.Charisma,
                    item.Intelligence, item.Wisdom
                });
        }
    }
}