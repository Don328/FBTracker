using FBTracker.Server.Data;
using FBTracker.Server.Data.Records;
using FBTracker.Server.Data.Repo;
using FBTracker.Server.Data.Services;
using FBTracker.Shared.GloblaConstants.EndpointTags;
using FBTracker.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace FBTracker.Server.Controllers;
[Route(StateRouteNames.controller_route)]
[ApiController]
public class StateController : ControllerBase
{
    private readonly StateService _stateService;

    public StateController(StateService stateService)
    {
        _stateService = stateService;
    }

    [HttpPost]
    [Route(StateRouteNames.selected_season)]
    public async Task<int> SelectedSeason([FromBody] int userId)
    {
        return await _stateService
            .SelectedSeason(userId);
    }

    [HttpPost]
    [Route(StateRouteNames.select_season)]
    public async Task SelectSeason([FromBody]Tuple<int, int> userId_season)
    {
        await _stateService.SelectSesason(
            new UserStateRecord(
                userId_season.Item1,
                userId_season.Item2));
    }

    [HttpPost]
    [Route(StateRouteNames.teams_confirmed)]
    public async Task<bool> TeamsConfirmed([FromBody]int season)
    {
        return await _stateService
            .TeamsConfirmed(season);
    }

    [HttpPost]
    [Route(StateRouteNames.schedule_confirmed)]
    public async Task<bool> ScheduleConfirmed([FromBody]int season)
    {
        return await _stateService
            .ScheduleConfirmed(season);
    }

    [HttpPost]
    [Route(StateRouteNames.confirm_teams_list)]
    public async Task ConfirmTeams([FromBody]int season)
    {
        await _stateService
            .ConfirmTeams(season);
    }
}
