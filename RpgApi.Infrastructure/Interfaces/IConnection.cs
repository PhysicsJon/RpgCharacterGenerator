using System.Data;

namespace RpgApi.Infrastructure.Interfaces
{
    public interface IConnection
    {
        IDbConnection GetDbConnection();
    }
}