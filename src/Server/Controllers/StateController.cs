using FBTracker.Server.Data;
using FBTracker.Server.Data.Records;
using FBTracker.Server.Data.Repo;
using FBTracker.Server.Data.Services;
using FBTracker.Shared.GloblaConstants.EndpointTags;
using FBTracker.Shared.Models;
using FBTracker.Shared.QueryObjects.State;
using Microsoft.AspNetCore.Mvc;

namespace FBTracker.Server.Controllers;
[Route(StateRouteNames.controller_route)]
[ApiController]
public class StateController : ControllerBase
{
    private readonly ILogger<StateController> _logger;
    private readonly StateService _stateService;

    public StateController(
        ILogger<StateController> logger,
        StateService stateService)
    {
        _logger = logger;
        _stateService = stateService;
    }

    [HttpPost]
    [Route(StateRouteNames.selected_season)]
    public async Task<int> SelectedSeason([FromBody]UserStateQuery query)
    {
        _logger.LogInformation($"Incomming request for selected season with userId: {query.Id}");
        return await _stateService
            .SelectedSeason(query.Id);
    }

    [HttpPost]
    [Route(StateRouteNames.select_season)]
    public async Task SelectSeason([FromBody]UserStateQuery query)
    {
        _logger.LogInformation($"Incomming request from user {query.Id} to change selected season to {query.SelectedSeason}");
        await _stateService.SelectSesason(
            new UserStateRecord(
                query.Id,
                query.SelectedSeason));
    }

    [HttpPost]
    [Route(StateRouteNames.teams_confirmed)]
    public async Task<bool> TeamsConfirmed([FromBody]UserStateQuery query)
    {
        _logger.LogInformation($"Incomming request for teams confirmed status for season: {query.SelectedSeason}");
        return await _stateService
            .TeamsConfirmed(query.SelectedSeason);
    }

    [HttpPost]
    [Route(StateRouteNames.schedule_confirmed)]
    public async Task<bool> ScheduleConfirmed([FromBody]UserStateQuery query)
    {
        _logger.LogInformation($"Incomming request for schedule confirmed status for season: {query.SelectedSeason}");
        return await _stateService
            .ScheduleConfirmed(query.SelectedSeason);
    }

    [HttpPost]
    [Route(StateRouteNames.confirm_teams_list)]
    public async Task ConfirmTeams([FromBody]UserStateQuery query)
    {
        _logger.LogInformation($"Incomming request to confirm teams list for season: {query.SelectedSeason}");
        await _stateService
            .ConfirmTeams(query.SelectedSeason);
    }
}
