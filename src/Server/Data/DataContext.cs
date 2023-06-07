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
        await _conn.OpenAsync();


        await _conn.CloseAsync();

        await Task.CompletedTask;
    }

    internal async Task<MySqlConnection> GetConnectionAsync()
    {
        await _conn.OpenAsync();
        return await Task.FromResult(_conn);
    }

    internal async Task CloseConnection()
    {
        await _conn.CloseAsync();
        await Task.CompletedTask;
    }
}
