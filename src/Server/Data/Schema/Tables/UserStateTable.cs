using FBTracker.Server.Data.Records;
using FBTracker.Server.Data.Schema.Commands;
using FBTracker.Server.Data.Schema.Constants;
using MySqlConnector;

namespace FBTracker.Server.Data.Schema.Tables;

internal class UserStateTable
{
    private readonly MySqlConnection _conn;
    private int _userId;

    public UserStateTable(MySqlConnection conn)
    {
        _conn = conn;
    }

    internal UserStateTable WithUserId(int userId)
    {
        _userId = userId;
        return this;
    }

    internal async Task<int> GetSelectedSeason()
    {
        int season = -1;
        using var cmd = _conn.CreateCommand();
        cmd.CommandText = ReadRow.userState_by_id;
        ParamBuilder.Build(cmd, ParameterNames.id, _userId);
        _conn.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            season = reader.GetInt32(1);
        }

        await _conn.CloseAsync();
        return await Task.FromResult(season);
    }

    internal async Task Update(
        UserStateRecord record)
    {
        using var cmd = _conn.CreateCommand();
        cmd.CommandText = UpdateRow.userState;
        ParamBuilder.Build(cmd, ParameterNames.id, record.Id);
        ParamBuilder.Build(cmd, ParameterNames.season, record.Season);

        _conn.Open();
        await cmd.ExecuteNonQueryAsync();
        await _conn.CloseAsync();

        await Task.CompletedTask;
    }
}
