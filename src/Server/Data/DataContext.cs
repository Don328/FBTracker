using FBTracker.Server.Data.Schema.Genesis;
using MySqlConnector;

namespace FBTracker.Server.Data;

public class DataContext
{
    private readonly MySqlConnection _conn;


    public DataContext(IConfiguration config)
    {
        _conn = new MySqlConnection(
            config.GetConnectionString("statDb"));
        
        EnsureDbExists().GetAwaiter();
    }

    private async Task EnsureDbExists()
    {
        MariaDbDatabase.EnsureExists(_conn);
        DbTables.EnsureExists(_conn);
        await Task.CompletedTask;
    }

    internal async Task<MySqlConnection> GetConnectionAsync()
    {
        return await Task.FromResult(_conn);
    }
}
