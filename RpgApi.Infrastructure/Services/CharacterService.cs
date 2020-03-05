using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using RpgApi.Domain.Models;
using RpgApi.Infrastructure.Interfaces;

namespace RpgApi.Infrastructure.Services
{
    public class CharacterService : ICrudService<Character, int>
    {
        private readonly IConnection _connection;

        public CharacterService(IConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Character> GetAll()
        {
            using var conn = _connection.GetDbConnection();
            var query = "SELECT c.*, d.* FROM Character c INNER JOIN CharacterDetail d on c.Id = d.CharacterId";
            return conn.Query<Character, CharacterDetail, Character>(query, (c, d) =>
            {
                c.CharacterDetail = d;
                return c;
            });
        }

        public Character GetById(int id)
        {
            using var conn = _connection.GetDbConnection();
            var query =
                "SELECT c.*, d.*, a.* FROM Character c INNER JOIN CharacterDetail d on c.Id = d.CharacterId INNER JOIN Attributes a on c.Id = a.CharacterId WHERE c.Id=@Id";
            return conn.Query<Character, CharacterDetail, Attributes, Character>(query, (c, d, a) =>
                {
                    c.CharacterDetail = d;
                    c.Attributes = a;
                    return c;
                },
                new {Id = id}).Single();
        }

        public Character Create(Character item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            using var conn = _connection.GetDbConnection();
            var query =
                @"INSERT INTO Character (CreatedOn) VALUES (GETUTCDATE());                  
                  SELECT CAST(SCOPE_IDENTITY() as int);
                ";

            var charId = conn.ExecuteScalar<int>(query);

            var detailsQuery =
                "INSERT INTO CharacterDetail(Id, Name, PlayerName, CharacterId) VALUES(NEWID(), @Name, @PlayerName, @CharacterId);";

            conn.Execute(detailsQuery,
                new {CharacterId = charId, item.CharacterDetail.Name, item.CharacterDetail.PlayerName});

            conn.Execute("INSERT INTO Attributes (Id, CharacterId) VALUES (NEWID(), @CharacterId)",
                new {CharacterId = charId});

            return GetById(charId);
        }

        public void Update(int id, Character item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            using var conn = _connection.GetDbConnection();
            var detailsQuery =
                @"UPDATE CharacterDetail SET Name=@Name, PlayerName=@PlayerName WHERE CharacterId=@CharacterId";
            conn.Execute(detailsQuery, item.CharacterDetail);
            var attributesQuery =
                @"UPDATE Attributes SET Strength=@Strength, Dexterity=@Dexterity, Constitution=@Constitution, Intelligence=@Intelligence, Wisdom=@Wisdom, Charisma=@Charisma WHERE CharacterId=@CharacterId";
            conn.Execute(attributesQuery, item.Attributes);
        }

        public void Delete(int id)
        {
            using var conn = _connection.GetDbConnection();

            conn.Execute("DELETE FROM Character where Id=@Id", new {Id = id});
        }
    }
}