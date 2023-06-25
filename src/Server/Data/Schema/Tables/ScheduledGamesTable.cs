using FBTracker.Server.Data.Mappers;
using FBTracker.Server.Data.Records;
using FBTracker.Server.Data.Schema.Commands;
using FBTracker.Server.Data.Schema.Constants;
using FBTracker.Shared.Models;
using MySqlConnector;

namespace FBTracker.Server.Data.Schema.Tables;

internal class ScheduledGamesTable
{
    private readonly MySqlConnection _conn;
    private int _season;
    private int _week;
    private int _teamId;

    public ScheduledGamesTable(
        MySqlConnection conn)
    {
        _conn = conn;
    }

    internal async Task<int> Create(ScheduledGame game)
    {
        int id = await TableIdService.GetNextId(
            _conn, GetIdsInUse.scheduledGame);

        int dayIdx = (int)game.GameDay;

        using var cmd = _conn.CreateCommand();
        cmd.CommandText = CreateRow.schedueldGame;
        ParamBuilder.Build(cmd, ParameterNames.id, id);
        ParamBuilder.Build(cmd, ParameterNames.season, game.Season);
        ParamBuilder.Build(cmd, ParameterNames.week, game.Week);
        ParamBuilder.Build(cmd, ParameterNames.homeTeamId, game.HomeTeamId);
        ParamBuilder.Build(cmd, ParameterNames.awayTeamId, game.AwayTeamId);
        ParamBuilder.Build(cmd, ParameterNames.byeTeamId, game.ByeTeamId);
        ParamBuilder.Build(cmd, ParameterNames.dayOfWeekIdx, dayIdx);

        _conn.Open();
        await cmd.ExecuteNonQueryAsync();
        await _conn.CloseAsync();
        
        return await Task.FromResult(id);
    }

    internal ScheduledGamesTable WithSeason(int season)
    {
        _season = season;
        return this;
    }

    internal ScheduledGamesTable WithWeek(int week)
    {
        _week = week;
        return this;
    }

    internal ScheduledGamesTable WithTeamId(int teamId)
    {
        _teamId = teamId;
        return this;
    }

    internal async Task<IEnumerable<ScheduledGame>> ReadSeason()
    {
        List<ScheduledGameRecord> records = default!;

        using var cmd = _conn.CreateCommand();
        cmd.CommandText = ReadTable.scheduledGames_bySeason;
        ParamBuilder.Build(cmd, ParameterNames.season, _season);

        _conn.Open();
        using var reader = await cmd.ExecuteReaderAsync();
        while (reader.Read())
        {
            records.Add(await ReadRecord(reader));
        }

        await _conn.CloseAsync();
        
        if (records is not null)
        {
            var entities = ScheduledGamesMapper.ToEntity(records);
            return await Task.FromResult(entities);
        }

        return Enumerable.Empty<ScheduledGame>();
    }

    internal async Task<IEnumerable<ScheduledGame>> ReadWeek()
    {
        List<ScheduledGameRecord> records = default!;

        using var cmd = _conn.CreateCommand();
        cmd.CommandText = ReadTable.scheduledGames_byWeek;
        ParamBuilder.Build(cmd, ParameterNames.season, _season);
        ParamBuilder.Build(cmd, ParameterNames.week, _week);

        _conn.Open();
        using var reader = await cmd.ExecuteReaderAsync();
        while (reader.Read())
        {
            records.Add(await ReadRecord(reader));
        }

        await _conn.CloseAsync();
        
        if (records is not null)
        {
            var entities = ScheduledGamesMapper.ToEntity(records);
            return await Task.FromResult(entities);
        }

        return Enumerable.Empty<ScheduledGame>();
    }

    internal async Task<IEnumerable<ScheduledGame>> ReadTeamSeason()
    {
        List<ScheduledGameRecord> records = new();
        
        using var cmd = _conn.CreateCommand();
        cmd.CommandText = ReadTable.scheduledGames_byTeam;
        
        ParamBuilder.Build(cmd, ParameterNames.season, _season);
        ParamBuilder.Build(cmd, ParameterNames.homeTeamId, _teamId);
        ParamBuilder.Build(cmd, ParameterNames.awayTeamId, _teamId);
        ParamBuilder.Build(cmd, ParameterNames.byeTeamId, _teamId);

        _conn.Open();
        using var reader = await cmd.ExecuteReaderAsync();
        while(reader.Read())
        {
            records.Add(await ReadRecord(reader));
        }
        
        await _conn.CloseAsync();

        if (records is not null)
        {
            var entities = ScheduledGamesMapper.ToEntity(records);
            return await Task.FromResult(entities);
        }

        return Enumerable.Empty<ScheduledGame>();
    }

    internal async Task<ScheduledGame> ReadTeamWeek()
    {
        ScheduledGameRecord record = default!;
        
        using var cmd = _conn.CreateCommand();
        cmd.CommandText = ReadRow.scheduledGames_week_byTeam;
        ParamBuilder.Build(cmd, ParameterNames.season, _season);
        ParamBuilder.Build(cmd, ParameterNames.week, _week);
        ParamBuilder.Build(cmd, PropertyNames.awayTeamId, _teamId);
        ParamBuilder.Build(cmd, PropertyNames.homeTeamId, _teamId);
        ParamBuilder.Build(cmd, PropertyNames.byeTeamId, _teamId);

        
        _conn.Open();
        using var reader = await cmd.ExecuteReaderAsync();
        
        while (reader.Read())
        {
            record = await ReadRecord(reader);
        }

        await _conn.CloseAsync();
        
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
