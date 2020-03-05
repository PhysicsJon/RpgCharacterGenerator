using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RpgApi.Infrastructure.Interfaces;

namespace RpgApi.Infrastructure.Utils
{
    public class Connection : IConnection
    {
        private readonly string connectionString;

        public Connection(IConfiguration config)
        {
            connectionString = config.GetConnectionString("NewConnString");
        }

        public IDbConnection GetDbConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}