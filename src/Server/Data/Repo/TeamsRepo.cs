using FBTracker.Server.Data.Mappers;
using FBTracker.Server.Data.Records;
using FBTracker.Server.Data.Schema;
using FBTracker.Server.Data.Schema.Commands;
using FBTracker.Server.Data.Schema.Constants;
using FBTracker.Server.Data.Schema.Tables;
using FBTracker.Shared.GloblaConstants;
using FBTracker.Shared.Models;
using MySqlConnector;

namespace FBTracker.Server.Data.Repo;

internal class TeamsRepo
{
    private readonly MySqlConnection _db;
    private int _season;
    private int _teamId;

    internal TeamsRepo(DataContext context)
    {
        _db = context.GetConnectionAsync()
            .GetAwaiter().GetResult();
    }

    internal TeamsRepo WithSeason(int season)
    {
        _season = season;
        return this;
    }

    internal TeamsRepo WithId(int teamId)
    {
        _teamId = teamId;
        return this;
    }

    internal async Task<Team> GetTeam()
    {
        return await TeamsTable.ReadFromId(_db, _teamId);
    }

    internal async Task<
        IEnumerable<Team>> GetSeasonTeams()
    {
        if (_season >= StateConstants.seasonMin &&
            _season <= StateConstants.seasonMax)
        {
            var teamRecords = await TeamsTable
                .ReadSeason(_db, _season);
            
            return TeamsMapper
                .ToEntity(teamRecords);
        }

        return await Task.FromResult(
            Enumerable.Empty<Team>());
    }

    internal async Task ConvertToSeason(
        int newSeason)
    {
        var teams = await GetSeasonTeams();

        foreach (var team in teams)
        {
            team.Season = newSeason;
            await TeamsTable.Create(_db, team);
        }

        await Task.CompletedTask;
    }

    internal async Task Update(Team team)
    {
        await TeamsTable.UpdateTeam(_db, team);
    }

    internal async Task Create(Team team)
    {
        await TeamsTable.Create(_db, team);
    }
}
