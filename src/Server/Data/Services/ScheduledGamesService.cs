using FBTracker.Server.Data.Repo;
using FBTracker.Shared.Models;

namespace FBTracker.Server.Data.Services;

public class ScheduledGamesService
{
    private readonly DataContext _db;

    public ScheduledGamesService(
        DataContext db)
    {
        _db = db;
    }

    public async Task ScheduleNewGame(ScheduledGame scheduledGame)
    {
        await new ScheduledGameRepo(_db)
            .Create(scheduledGame);
    }

    public async Task<IEnumerable<ScheduledGame>> GetSeason(int season)
    {
        return await new ScheduledGameRepo(_db)
            .WithSeason(season)
            .GetSeason();
    }

    public async Task<IEnumerable<ScheduledGame>> GetWeek(
        int season, 
        int week)
    {
        return await new ScheduledGameRepo(_db)
            .WithSeason(season)
            .WithWeek(week)
            .GetWeek();
    }

    public async Task<IEnumerable<ScheduledGame>> GetTeamSeason(
        int season,
        int teamId)
    {
        return await new ScheduledGameRepo(_db)
            .WithSeason(season)
            .WithTeamId(teamId)
            .GetTeamSeason();
    }

    public async Task<ScheduledGame> GetTeamWeek(
        int season,
        int week, 
        int teamId)
    {
        return await new ScheduledGameRepo(_db)
            .WithSeason(season)
            .WithWeek(week)
            .WithTeamId(teamId)
            .GetTeamWeek();
    }
}
