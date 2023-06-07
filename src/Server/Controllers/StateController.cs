using FBTracker.Server.Data;
using FBTracker.Server.Data.Repo;
using FBTracker.Server.State;
using FBTracker.Shared.HardValues.EndpointTags;
using FBTracker.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace FBTracker.Server.Controllers;
[Route(StateRouteNames.controller_route)]
[ApiController]
public class StateController : ControllerBase
{
    private readonly AppState _state;
    private readonly DataContext _db = default!;

    public StateController(
        AppState appState,
        DataContext db)
    {
        _state = appState;
        _db = db;
    }

    [HttpGet]
    [Route(StateRouteNames.seasonSelected)]
    public async Task<ActionResult<bool>> SeasonIsSelected()
    {
        var isSet = _state.SeasonIsSet();
        return await Task.FromResult(Ok(isSet));
    }

    [HttpGet]
    [Route(StateRouteNames.season)]
    public async Task<int> GetSelectedSeason()
    {
        return  await Task.FromResult(_state.Season);
    }

    [HttpPost]
    [Route(StateRouteNames.season)]
    public async Task SetSelectedSeason(
        [FromBody]int season)
    {
        _state.SetSeason(season);
 
        var teams = await new TeamsRepo(_db)
            .FromSeason(season).ToList();
        
        _state.LoadTeams(teams);

        await Task.CompletedTask;
    }

    [HttpGet]
    [Route(StateRouteNames.teamsLoaded)]
    public async Task<bool> TeamsListIsSet()
    {
        var isSet = _state.TeamsLoaded();
        return await Task.FromResult(isSet);
    }

    [HttpGet]
    [Route(StateRouteNames.teams)]
    public async Task<IEnumerable<Team>> GetTeams()
    {
        if (_state.TeamsLoaded())
        {
            return await Task.FromResult(
                _state.Teams);
        }

        return await Task.FromResult(
            Enumerable.Empty<Team>());
    }
}
