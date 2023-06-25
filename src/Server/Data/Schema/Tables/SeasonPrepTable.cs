using FBTracker.Server.Data.Records;
using FBTracker.Server.Data.Schema.Commands;
using FBTracker.Server.Data.Schema.Constants;
using MySqlConnector;
using System.Security.Cryptography;

namespace FBTracker.Server.Data.Schema.Tables;

internal class SeasonPrepTable
{
    private readonly MySqlConnection _conn;
    private int _season;

    public SeasonPrepTable(MySqlConnection conn)
    {
        _conn = conn;
    }

    internal async Task Create(SeasonPrepRecord record)
    {
        int id = await TableIdService.GetNextId(
            _conn, GetIdsInUse.seasonPrep);

        using (var cmd = _conn.CreateCommand())
        {

            cmd.CommandText = CreateRow.seasonPrep;
            ParamBuilder.Build(cmd, ParameterNames.id, id);
            ParamBuilder.Build(cmd, ParameterNames.season, record.Season);
            ParamBuilder.Build(cmd, ParameterNames.teamsConfirmed, record.TeamsConfirmed);
            ParamBuilder.Build(cmd, ParameterNames.schedulesLoaded, record.ScheduleConfirmed);

            _conn.Open();
            await cmd.ExecuteNonQueryAsync();
            await _conn.CloseAsync();
        }

        await Task.CompletedTask;
    }

    internal SeasonPrepTable WithSeason(int season)
    {
        _season = season;
        return this;
    }

    internal async Task<SeasonPrepRecord> ReadRecord()
    {
        SeasonPrepRecord record = default!;

        using (var cmd = _conn.CreateCommand())
        {

            cmd.CommandText = ReadRow.seasonPrep_bySeason;
            ParamBuilder.Build(cmd, ParameterNames.season, _season);

            _conn.Open();
            using var reader = await cmd.ExecuteReaderAsync();

            while (reader.Read())
            {
                record = await ReadRecord(reader);
            }

            await _conn.CloseAsync();
        }

        return await Task.FromResult(record);
    }

    internal async Task<IEnumerable<SeasonPrepRecord>> ReadAll()
    {
        var records = new List<SeasonPrepRecord>();

        using var cmd = _conn.CreateCommand();
        cmd.CommandText = ReadTable.seasonPrep;

        _conn.Open();
        using var reader = await cmd.ExecuteReaderAsync();
        while (reader.Read())
        {
            records.Add(await ReadRecord(reader));
        }

        await _conn.CloseAsync();

        return await Task.FromResult(records);
    }

    internal async Task ConfirmSeasonTeams()
    {
        var record = await ReadRecord();
        using var cmd = _conn.CreateCommand();
        cmd.CommandText = UpdateRow.seasonPrep;
        ParamBuilder.Build(cmd, ParameterNames.id, record.Id);
        ParamBuilder.Build(cmd, ParameterNames.season, _season);
        ParamBuilder.Build(cmd, ParameterNames.teamsConfirmed, 1);
        ParamBuilder.Build(cmd, ParameterNames.schedulesLoaded, record.ScheduleConfirmed);

        _conn.Open();
        await cmd.ExecuteNonQueryAsync();
        await _conn.CloseAsync();
    }

    internal async Task ConfirmSeasonSchedule()
    {
        var record = await ReadRecord();
        using var cmd = _conn.CreateCommand();
        cmd.CommandText = UpdateRow.seasonPrep;
        ParamBuilder.Build(cmd, ParameterNames.id, record.Id);
        ParamBuilder.Build(cmd, ParameterNames.season, _season);
        ParamBuilder.Build(cmd, ParameterNames.teamsConfirmed, record.TeamsConfirmed);
        ParamBuilder.Build(cmd, ParameterNames.schedulesLoaded, 1);

        _conn.Open();
        await cmd.ExecuteNonQueryAsync();
        await _conn.CloseAsync();
    }

    private static async Task<SeasonPrepRecord> ReadRecord(
        MySqlDataReader reader)
    {
        var record = new SeasonPrepRecord(
            Id: reader.GetInt32(0),
            Season: reader.GetInt32(1),
            TeamsConfirmed: reader.GetBoolean(2),
            ScheduleConfirmed: reader.GetBoolean(3));

        return await Task.FromResult(record);
    }
}
