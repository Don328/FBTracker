using FBTracker.Server.Data.Schema.Commands;
using MySqlConnector;

namespace FBTracker.Server.Data.Schema.Genesis;

internal static class MariaDbDatabase
{
    internal static async void EnsureExists(MySqlConnection conn)
    {
        conn.Open();
        new MySqlCommand(CreateDb.statDb, conn).ExecuteNonQuery();
        await conn.CloseAsync();
    }
}
