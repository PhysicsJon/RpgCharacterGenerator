using Dapper;
using RpgApi.Domain.Models;
using RpgApi.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpgApi.Infrastructure.Services
{
    public class CharacterServiceAsync : ICrudServiceAsync<Character, int>
    {
        private readonly IConnection _connection;

        public CharacterServiceAsync(IConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Character>> GetAll()
        {
            using var conn = _connection.GetDbConnection();
            var query = "SELECT c.*, d.* FROM Character c INNER JOIN CharacterDetail d on c.Id = d.CharacterId";
            return await conn.QueryAsync<Character, CharacterDetail, Character>(query, (c, d) =>
            {
                c.CharacterDetail = d;
                return c;
            }).ConfigureAwait(false);
        }

        public async Task<Character> GetById(int id)
        {
            using var conn = _connection.GetDbConnection();
            var query =
                "SELECT c.*, d.*, a.* FROM Character c INNER JOIN CharacterDetail d on c.Id = d.CharacterId INNER JOIN Attributes a on c.Id = a.CharacterId WHERE c.Id=@Id";
            var result = await conn.QueryAsync<Character, CharacterDetail, Attributes, Character>(query, (c, d, a) =>
                {
                    c.CharacterDetail = d;
                    c.Attributes = a;
                    return c;
                },
                new { Id = id }).ConfigureAwait(false);

            return result.Single();
        }

        public async Task<Character> Create(Character item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            using var conn = _connection.GetDbConnection();
            var query =
                @"INSERT INTO Character (CreatedOn) VALUES (GETUTCDATE());                  
                  SELECT CAST(SCOPE_IDENTITY() as int);
                ";

            var charId = await conn.ExecuteScalarAsync<int>(query).ConfigureAwait(false);

            var detailsQuery =
                "INSERT INTO CharacterDetail(Id, Name, PlayerName, CharacterId) VALUES(NEWID(), @Name, @PlayerName, @CharacterId);";

            await conn.ExecuteAsync(detailsQuery,
                new { CharacterId = charId, item.CharacterDetail.Name, item.CharacterDetail.PlayerName }).ConfigureAwait(false);

            await conn.ExecuteAsync("INSERT INTO Attributes (Id, CharacterId) VALUES (NEWID(), @CharacterId)",
                new { CharacterId = charId }).ConfigureAwait(false);

            return await GetById(charId).ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id)
        {
            using var conn = _connection.GetDbConnection();

            await conn.ExecuteAsync("DELETE FROM Character where Id=@Id", new { Id = id }).ConfigureAwait(false);
        }
    }
}