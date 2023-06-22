using FBTracker.Server.Data.Repo;
using FBTracker.Shared.Models;

namespace FBTracker.Server.Data.Services;

public class TeamsService
{
    private readonly DataContext _db;

    public TeamsService(
        DataContext db)
    {
        _db = db;
    }

    internal async Task<IEnumerable<Team>> GetTeams(int season)
    {
        return await new TeamsRepo(_db)
            .WithSeason(season)
            .GetSeasonTeams();
    }

    internal async Task<IEnumerable<Team>> ConvertTeamsFromSeason(int toSeason, int fromSeason)
    {
        await new TeamsRepo(_db)
            .WithSeason(fromSeason)
            .ConvertToSeason(toSeason);

        return await GetTeams(toSeason);
    }

    internal async Task UpdateTeamData(Team team)
    {
        await new TeamsRepo(_db).Update(team);
    }

    internal async Task AddTeam(Team team)
    {
        await new TeamsRepo(_db).Create(team);
    }
}