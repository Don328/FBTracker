using FBTracker.Server.Data.Schema.Commands;
using MySqlConnector;

namespace FBTracker.Server.Data.Schema.Genesis;

internal static class MariaDbDatabase
{
    internal static void EnsureExists(MySqlConnection conn)
    {
        new MySqlCommand(CreateDb.statDb, conn).ExecuteNonQuery();
    }
}
