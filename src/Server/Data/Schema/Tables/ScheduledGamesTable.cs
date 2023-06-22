using FBTracker.Server.Data.Mappers;
using FBTracker.Server.Data.Records;
using FBTracker.Server.Data.Schema.Commands;
using FBTracker.Server.Data.Schema.Constants;
using FBTracker.Shared.Models;
using MySqlConnector;

namespace FBTracker.Server.Data.Schema.Tables;

internal static class ScheduledGamesTable
{
    internal static async Task<int> Create(
        MySqlConnection conn, ScheduledGame game)
    {
        int id = await TableIdService.GetNextId(
            conn, GetIdsInUse.scheduledGame);

        int dayIdx;
        if (game.GameDay == null)
            dayIdx = -1;
        else
            dayIdx = (int)game.GameDay;

        using var cmd = conn.CreateCommand();
        cmd.CommandText = CreateRow.schedueldGame;
        ParamBuilder.Build(cmd, ParameterNames.id, id);
        ParamBuilder.Build(cmd, ParameterNames.season, game.Season);
        ParamBuilder.Build(cmd, ParameterNames.week, game.Week);
        ParamBuilder.Build(cmd, ParameterNames.homeTeamId, game.HomeTeamId);
        ParamBuilder.Build(cmd, ParameterNames.awayTeamId, game.AwayTeamId);
        ParamBuilder.Build(cmd, ParameterNames.byeTeamId, game.ByeTeamId);
        ParamBuilder.Build(cmd, ParameterNames.dayOfWeekIdx, dayIdx);

        conn.Open();
        await cmd.ExecuteNonQueryAsync();
        await conn.OpenAsync();
        
        return await Task.FromResult(id);
    }

    internal static async Task<IEnumerable<ScheduledGame>> ReadSeason(
        MySqlConnection conn,
        int season)
    {
        List<ScheduledGameRecord> records = default!;

        using var cmd = conn.CreateCommand();
        cmd.CommandText = ReadTable.scheduledGames_bySeason;
        ParamBuilder.Build(cmd, ParameterNames.season, season);

        conn.Open();
        using var reader = await cmd.ExecuteReaderAsync();
        while (reader.Read())
        {
            records.Add(await ReadRecord(reader));
        }

        if (records is not null)
        {
            var entities = ScheduledGamesMapper.ToEntity(records);
            return await Task.FromResult(entities);
        }

        return Enumerable.Empty<ScheduledGame>();
    }

    internal static async Task<IEnumerable<ScheduledGame>> ReadWeek(
        MySqlConnection conn,
        int season,
        int week)
    {
        List<ScheduledGameRecord> records = default!;

        using var cmd = conn.CreateCommand();
        cmd.CommandText = ReadTable.scheduledGames_byWeek;
        ParamBuilder.Build(cmd, ParameterNames.season, season);
        ParamBuilder.Build(cmd, ParameterNames.week, week);

        conn.Open();
        using var reader = await cmd.ExecuteReaderAsync();
        while (reader.Read())
        {
            records.Add(await ReadRecord(reader));
        }

        if (records is not null)
        {
            var entities = ScheduledGamesMapper.ToEntity(records);
            return await Task.FromResult(entities);
        }

        return Enumerable.Empty<ScheduledGame>();
    }

    internal static async Task<IEnumerable<ScheduledGame>> ReadTeamSeason(
        MySqlConnection conn,
        int teamId,
        int season)
    {
        List<ScheduledGameRecord> records = new();
        
        using var cmd = conn.CreateCommand();
        cmd.CommandText = ReadTable.scheduledGames_byTeam;
        
        ParamBuilder.Build(cmd, ParameterNames.season, season);
        ParamBuilder.Build(cmd, ParameterNames.homeTeamId, teamId);
        ParamBuilder.Build(cmd, ParameterNames.awayTeamId, teamId);
        ParamBuilder.Build(cmd, ParameterNames.byeTeamId, teamId);

        conn.Open();
        using var reader = await cmd.ExecuteReaderAsync();
        while(reader.Read())
        {
            records.Add(await ReadRecord(reader));
        }
        
        await conn.CloseAsync();

        if (records is not null)
        {
            var entities = ScheduledGamesMapper.ToEntity(records);
            return await Task.FromResult(entities);
        }

        return Enumerable.Empty<ScheduledGame>();
    }

    internal static async Task<ScheduledGame> ReadTeamWeek(
        MySqlConnection conn,
        int teamId,
        int season,
        int week)
    {
        ScheduledGameRecord record = default!;
        
        using var cmd = conn.CreateCommand();
        cmd.CommandText = ReadRow.scheduledGames_week_byTeam;
        ParamBuilder.Build(cmd, ParameterNames.season, season);
        ParamBuilder.Build(cmd, ParameterNames.week, week);
        ParamBuilder.Build(cmd, PropertyNames.awayTeamId, teamId);
        ParamBuilder.Build(cmd, PropertyNames.homeTeamId, teamId);
        ParamBuilder.Build(cmd, PropertyNames.byeTeamId, teamId);

        
        conn.Open();
        using var reader = await cmd.ExecuteReaderAsync();
        
        while (reader.Read())
        {
            record = await ReadRecord(reader);
        }

        await conn.CloseAsync();
        
        if (record is not null)
        {
            return await Task.FromResult(ScheduledGamesMapper.ToEntity(record));
        }

        return new ScheduledGame();
    }

    private static async Task<ScheduledGameRecord> 
        ReadRecord(MySqlDataReader reader)
    {
        var record = new ScheduledGameRecord(
             Id: reader.GetInt32(0),
             Season: reader.GetInt32(1),
             Week: reader.GetInt32(2),
             HomeTeamId: reader.GetInt32(3),
             AwayTeamId: reader.GetInt32(4),
             ByeTeamId: reader.GetInt32(5),
             DayOfWeekIdx: reader.GetInt32(6));

        return await Task.FromResult(record);
    }
}
