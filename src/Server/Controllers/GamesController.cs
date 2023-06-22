using FBTracker.Server.Data.Services;
using FBTracker.Shared.GloblaConstants.EndpointTags;
using FBTracker.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FBTracker.Server.Controllers;
[Route(ScheduledGamesRouteNames.controller_route)]
[ApiController]
public class GamesController : ControllerBase
{
    private ScheduledGamesService _scheduledGamesService;

    public GamesController(ScheduledGamesService scheduledGamesService)
    {
        _scheduledGamesService = scheduledGamesService;
    }

    [HttpPost]
    [Route(ScheduledGamesRouteNames.get_season)]
    public async Task<IEnumerable<ScheduledGame>> GetSeason([FromBody]int season)
    {
        return await _scheduledGamesService.GetSeason(season);
    }

    [HttpPost]
    [Route(ScheduledGamesRouteNames.get_week)]
    public async Task<IEnumerable<ScheduledGame>> GetWeek([FromBody] Tuple<int, int> season_week)
    {
        return await _scheduledGamesService.GetWeek(
            season_week.Item1, 
            season_week.Item2);
    }

    [HttpPost]
    [Route(ScheduledGamesRouteNames.get_team_season)]
    public async Task<IEnumerable<ScheduledGame>> GetTeamSeason([FromBody]Tuple<int, int> season_team)
    {
        return await _scheduledGamesService.GetTeamSeason(
            season_team.Item1,
            season_team.Item2);
    }

    [HttpPost]
    [Route(ScheduledGamesRouteNames.get_team_week)]
    public async Task<ScheduledGame> GetTeamWeek([FromBody] int[] season_week_team)
    {
        return await _scheduledGamesService.GetTeamWeek(
            season: season_week_team[0],
            week: season_week_team[1], 
            teamId: season_week_team[2]);  
    }

    [HttpPost]
    [Route(ScheduledGamesRouteNames.save_new_record)]
    public async Task SaveNewRecord([FromBody]ScheduledGame game)
    {

        await _scheduledGamesService.ScheduleNewGame(game);
    }
}
