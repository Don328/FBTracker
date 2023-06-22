using FBTracker.Server.Data.Mappers;
using FBTracker.Server.Data.Schema.Tables;
using FBTracker.Shared.Models;
using MySqlConnector;

namespace FBTracker.Server.Data.Repo;

internal class ScheduledGameRepo
{
    private readonly MySqlConnection _db;
    private int _season;
    private int _week;
    private int _teamId;

    public ScheduledGameRepo(DataContext context)
    {
        _db = context.GetConnectionAsync()
            .GetAwaiter().GetResult();
    }

    internal ScheduledGameRepo WithSeason(int season)
    {
        _season = season;
        return this;
    }

    internal ScheduledGameRepo WithWeek(int week)
    { 
        _week = week; 
        return this; 
    }

    internal ScheduledGameRepo WithTeamId(int teamId)
    {
        _teamId = teamId;
        return this;
    }

    internal async Task Create(ScheduledGame scheduledGame)
    {
        await ScheduledGamesTable.Create(_db, scheduledGame);
        await Task.CompletedTask;
    }

    internal async Task<IEnumerable<ScheduledGame>> GetTeamSeason()
    {
        return await ScheduledGamesTable.ReadTeamSeason(
            conn: _db, teamId: _teamId, season: _season);
    }

    internal async Task<ScheduledGame> GetTeamWeek()
    {
        return await ScheduledGamesTable.ReadTeamWeek(
            conn: _db, teamId: _teamId, season: _season, week: _week);
    }

    internal async Task<IEnumerable<ScheduledGame>> GetSeason()
    {
        return await ScheduledGamesTable.ReadSeason(
            conn: _db, season: _season);
    }

    internal async Task<IEnumerable<ScheduledGame>> GetWeek()
    {
        return await ScheduledGamesTable.ReadWeek(
            conn: _db, season: _season, week: _week);
    }
}
