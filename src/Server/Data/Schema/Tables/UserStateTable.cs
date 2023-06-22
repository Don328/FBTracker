using FBTracker.Server.Data.Records;
using FBTracker.Server.Data.Schema.Commands;
using FBTracker.Server.Data.Schema.Constants;
using MySqlConnector;

namespace FBTracker.Server.Data.Schema.Tables;

internal static class UserStateTable
{
    internal static async Task<int> GetSelectedSeason(
        MySqlConnection conn,
        int userId)
    {
        int season = -1;
        using var cmd = conn.CreateCommand();
        cmd.CommandText = ReadRow.userState_by_id;
        ParamBuilder.Build(cmd, ParameterNames.id, userId);
        conn.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            season = reader.GetInt32(1);
        }

        await conn.CloseAsync();
        return await Task.FromResult(season);
    }

    internal static async Task Update(
        MySqlConnection conn,
        UserStateRecord record)
    {
        using var cmd = conn.CreateCommand();
        cmd.CommandText = UpdateRow.userState;
        ParamBuilder.Build(cmd, ParameterNames.id, record.Id);
        ParamBuilder.Build(cmd, ParameterNames.season, record.Season);

        conn.Open();
        await cmd.ExecuteNonQueryAsync();
        await conn.CloseAsync();

        await Task.CompletedTask;
    }
}
