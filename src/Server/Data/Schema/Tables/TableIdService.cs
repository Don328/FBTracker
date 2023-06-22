using FBTracker.Server.Data.Schema.Commands;
using MySqlConnector;

namespace FBTracker.Server.Data.Schema.Tables;

internal static class TableIdService
{
    internal static async Task<int> GetNextId(
        MySqlConnection conn,
        string cmdText)
    {
        using (var cmd = conn.CreateCommand())
        {
            List<int> idList = new();

            cmd.CommandText = cmdText;

            conn.Open();
            using var reader = await cmd.ExecuteReaderAsync();
            
            while (reader.Read())
            {
                idList.Add(reader.GetInt32(0));
            }

            await conn.CloseAsync();
            if (idList.Any())
            {
                return await Task.FromResult(idList.Last() +1);
            }

            return 1;
        }


    }
}
