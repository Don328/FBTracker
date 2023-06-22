using FBTracker.Server.Data.Records;
using FBTracker.Server.Data.Schema.Commands;
using FBTracker.Server.Data.Schema.Constants;
using MySqlConnector;
using System.Security.Cryptography;

namespace FBTracker.Server.Data.Schema.Tables;

internal static class SeasonPrepTable
{
    internal static async Task Create(
        MySqlConnection conn, SeasonPrepRecord record)
    {
        int id = await TableIdService.GetNextId(
            conn, GetIdsInUse.seasonPrep);

        using (var cmd = conn.CreateCommand())
        {

            cmd.CommandText = CreateRow.seasonPrep;
            ParamBuilder.Build(cmd, ParameterNames.id, id);
            ParamBuilder.Build(cmd, ParameterNames.season, record.Season);
            ParamBuilder.Build(cmd, ParameterNames.teamsConfirmed, record.TeamsConfirmed);
            ParamBuilder.Build(cmd, ParameterNames.schedulesLoaded, record.ScheduleConfirmed);

            conn.Open();
            await cmd.ExecuteNonQueryAsync();
            await conn.CloseAsync();
        }

        await Task.CompletedTask;
    }

    internal static async Task<SeasonPrepRecord> ReadRecord(
        MySqlConnection conn, int season)
    {
        SeasonPrepRecord record = default!;
        int record_id = -1;
        int record_season = -1;
        bool record_teamsConfirmed = false;
        bool record_schedulesLoaded = false;

        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = ReadRow.seasonPrep_bySeason;
            ParamBuilder.Build(cmd, ParameterNames.season, season);

            conn.Open();
            using var reader = await cmd.ExecuteReaderAsync();

            while (reader.Read())
            {
                record_id = reader.GetInt32(0);
                record_season = reader.GetInt32(1);
                record_teamsConfirmed = reader.GetBoolean(2);
                record_schedulesLoaded = reader.GetBoolean(3);
            }

            await conn.CloseAsync();

            record = new SeasonPrepRecord(
                Id: record_id,
                Season: record_season,
                TeamsConfirmed: record_teamsConfirmed,
                ScheduleConfirmed: record_schedulesLoaded);

        }

        return await Task.FromResult(record);
    }

    internal static async Task<IEnumerable<SeasonPrepRecord>> ReadAll(
        MySqlConnection conn)
    {
        var records = new List<SeasonPrepRecord>();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = ReadTable.seasonPrep;

        conn.Open();
        using var reader = await cmd.ExecuteReaderAsync();
        while (reader.Read())
        {
            records.Add(new SeasonPrepRecord(
                Id: reader.GetInt32(0),
                Season: reader.GetInt32(1),
                TeamsConfirmed: reader.GetBoolean(2),
                ScheduleConfirmed: reader.GetBoolean(3)));
        }

        await conn.CloseAsync();

        return await Task.FromResult(records);
    }

    internal static async Task ConfirmSeasonTeams(
        MySqlConnection conn, int season)
    {
        var record = await ReadRecord(conn, season);
        using var cmd = conn.CreateCommand();
        cmd.CommandText = UpdateRow.seasonPrep;
        ParamBuilder.Build(cmd, ParameterNames.id, record.Id);
        ParamBuilder.Build(cmd, ParameterNames.season, season);
        ParamBuilder.Build(cmd, ParameterNames.teamsConfirmed, 1);
        ParamBuilder.Build(cmd, ParameterNames.schedulesLoaded, record.ScheduleConfirmed);

        conn.Open();
        await cmd.ExecuteNonQueryAsync();
        await conn.CloseAsync();
    }

    internal static async Task ConfirmSeasonSchedule(
    MySqlConnection conn, int season)
    {
        var record = await ReadRecord(conn, season);
        using var cmd = conn.CreateCommand();
        cmd.CommandText = UpdateRow.seasonPrep;
        ParamBuilder.Build(cmd, ParameterNames.id, record.Id);
        ParamBuilder.Build(cmd, ParameterNames.season, season);
        ParamBuilder.Build(cmd, ParameterNames.teamsConfirmed, record.TeamsConfirmed);
        ParamBuilder.Build(cmd, ParameterNames.schedulesLoaded, 1);

        conn.Open();
        await cmd.ExecuteNonQueryAsync();
        await conn.CloseAsync();
    }
}
