using FBTracker.Server.Data.Services;
using FBTracker.Shared.GloblaConstants.EndpointTags;
using FBTracker.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FBTracker.Server.Controllers;
[Route(TeamsRouteNames.controller_route)]
[ApiController]
public class TeamsController : ControllerBase
{
    private readonly TeamsService _teamsService;

    public TeamsController(TeamsService teamsService)
    {
        _teamsService = teamsService;
    }

    [HttpPost]
    [Route(TeamsRouteNames.get)]
    public async Task<IEnumerable<Team>> GetTeams([FromBody]int season)
    {
        return await _teamsService.GetTeams(season);
    }

    [HttpPost]
    [Route(TeamsRouteNames.add)]
    public async Task Add([FromBody] Team team)
    {
        await _teamsService.AddTeam(team);
        await Task.CompletedTask;
    }

    [HttpPost]
    [Route(TeamsRouteNames.update)]
    public async Task Update([FromBody] Team team)
    {
        await _teamsService.UpdateTeamData(team);
    }

    [HttpPost]
    [Route(TeamsRouteNames.load_from_season)]
    public async Task<IEnumerable<Team>> LoadPreviousTeams(
        [FromBody] 
        int fromSeason, 
        int toSeason)
    {
        return await _teamsService.ConvertTeamsFromSeason(
            fromSeason:fromSeason, 
            toSeason:toSeason);
    }
}
