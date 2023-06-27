using FBTracker.Server.Data.Services;
using FBTracker.Shared.GloblaConstants.EndpointTags;
using FBTracker.Shared.Models;
using FBTracker.Shared.QueryObjects.Games;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FBTracker.Server.Controllers;
[Route(ScheduledGamesRouteNames.controller_route)]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly ILogger<GamesController> _logger;
    private ScheduledGamesService _scheduledGamesService;

    public GamesController(
        ILogger<GamesController> logger,
        ScheduledGamesService scheduledGamesService)
    {
        _logger = logger;
        _scheduledGamesService = scheduledGamesService;
    }

    [HttpPost]
    [Route(ScheduledGamesRouteNames.get_season)]
    public async Task<IEnumerable<ScheduledGame>> GetSeason([FromBody]GamesQuery query)
    {
        _logger.LogInformation($"Incomming request for Scheduled games from season: {query.Season}");
        return await _scheduledGamesService.GetSeason(query.Season);
    }

    [HttpPost]
    [Route(ScheduledGamesRouteNames.get_week)]
    public async Task<IEnumerable<ScheduledGame>> GetWeek([FromBody]GamesQuery query)
    {
        _logger.LogInformation($"Incomming request for games. Season: {query.Season}, Week: {query.Week}");
        return await _scheduledGamesService.GetWeek(
            query.Season, 
            query.Week);
    }

    [HttpPost]
    [Route(ScheduledGamesRouteNames.get_team_season)]
    public async Task<IEnumerable<ScheduledGame>> GetTeamSeason([FromBody]GamesQuery query)
    {
        _logger.LogInformation($"Incomming request for games. Season: {query.Season}, Team:{query.TeamId}");
        var games = await _scheduledGamesService.GetTeamSeason(
            query.Season,
            query.TeamId);

        return await Task.FromResult(games);
    }

    [HttpPost]
    [Route(ScheduledGamesRouteNames.get_team_week)]
    public async Task<ScheduledGame> GetTeamWeek([FromBody]GamesQuery query)
    {
        _logger.LogInformation($"Incomming request for game. Season: {query.Season}, Week: {query.Week}, Team:{query.TeamId}");
        return await _scheduledGamesService.GetTeamWeek(
            season: query.Season,
            week: query.Week, 
            teamId: query.TeamId);  
    }

    [HttpPost]
    [Route(ScheduledGamesRouteNames.save_new_record)]
    public async Task SaveNewRecord([FromBody]ScheduledGame game)
    {
        _logger.LogInformation($"Incomming record to save. Season: {game.Season}, Week: {game.Week} | {game.AwayTeamId} vs. {game.HomeTeamId}");
        await _scheduledGamesService.ScheduleNewGame(game);
    }
}
