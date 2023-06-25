using FBTracker.Server.Data.Services;
using FBTracker.Shared.GloblaConstants.EndpointTags;
using FBTracker.Shared.Models;
using FBTracker.Shared.QueryObjects.Teams;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FBTracker.Server.Controllers;
[Route(TeamsRouteNames.controller_route)]
[ApiController]
public class TeamsController : ControllerBase
{
    private readonly ILogger<TeamsService> _logger;
    private readonly TeamsService _teamsService;

    public TeamsController(
        ILogger<TeamsService> logger,
        TeamsService teamsService)
    {
        _logger = logger;
        _teamsService = teamsService;
    }

    [HttpPost]
    [Route(TeamsRouteNames.get)]
    public async Task<IEnumerable<Team>> GetTeams([FromBody]TeamsQuery query)
    {
        _logger.LogInformation($"Incomming request for teams from season: {query.Season}");
        return await _teamsService.GetTeams(query.Season);
    }

    [HttpPost]
    [Route(TeamsRouteNames.add)]
    public async Task Add([FromBody] Team team)
    {
        _logger.LogInformation($"Incomming request to save new team:{team.Season} | {team.Locale} {team.Name} | {team.Abrev}");
        await _teamsService.AddTeam(team);
        await Task.CompletedTask;
    }

    [HttpPost]
    [Route(TeamsRouteNames.update)]
    public async Task Update([FromBody] Team team)
    {
        _logger.LogInformation($"Incomming request to update team data:{team.Season} | {team.Id} | {team.Locale} {team.Name} | {team.Abrev}");
        await _teamsService.UpdateTeamData(team);
    }

    [HttpPost]
    [Route(TeamsRouteNames.load_from_season)]
    public async Task<IEnumerable<Team>> LoadPreviousTeams(
        [FromBody]SeasonTeamTransfer query)
    {
        _logger.LogInformation($"Incomming request to load teams from season: {query.LoadFromSeason} to season: {query.LoadToSeason}");
        return await _teamsService.ConvertTeamsFromSeason(
            fromSeason:query.LoadFromSeason, 
            toSeason:query.LoadToSeason);
    }
}
