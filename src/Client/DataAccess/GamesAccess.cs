using FBTracker.Shared.GloblaConstants.EndpointTags;
using FBTracker.Shared.Models;
using System.Net.Http.Json;

namespace FBTracker.Client.DataAccess;

internal static class GamesAccess
{
    internal static async Task<IEnumerable<ScheduledGame>> GetSeasonSchedule(
        HttpClient http,
        int season)
    {
        var url = http.BaseAddress +
            ScheduledGamesRouteNames.controller_route + "/" +
            ScheduledGamesRouteNames.get_season;

        var response = await http
            .PostAsJsonAsync<int>(url, season);

        if (response.IsSuccessStatusCode)
        {
            return (IEnumerable<ScheduledGame>)response.Content;
        }

        return Enumerable.Empty<ScheduledGame>();
    }

    internal static async Task<IEnumerable<ScheduledGame>> GetWeekSchedule(
        HttpClient http,
        int season,
        int week)
    {
        var url = http.BaseAddress +
            ScheduledGamesRouteNames.controller_route + "/" +
            ScheduledGamesRouteNames.get_week;

        var season_week = new Tuple<int, int>(season, week);

        var response = await http
            .PostAsJsonAsync(
            url, season_week);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content
                .ReadFromJsonAsync<IEnumerable<ScheduledGame>>() 
                ?? Enumerable.Empty<ScheduledGame>();
        }

        return Enumerable.Empty<ScheduledGame>();
    }

    internal static async Task<IEnumerable<ScheduledGame>> GetTeamSeason(
        HttpClient http,
        int season,
        int teamId)
    {
        var url = http.BaseAddress +
            ScheduledGamesRouteNames.controller_route + "/" +
            ScheduledGamesRouteNames.get_team_season;

        var season_team = new Tuple<int, int>(season, teamId);

        var response = await http
            .PostAsJsonAsync(
            url, season_team);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content
                .ReadFromJsonAsync<IEnumerable<ScheduledGame>>()
                ?? Enumerable.Empty<ScheduledGame>();
        }

        return Enumerable.Empty<ScheduledGame>();
    }

    internal static async Task<ScheduledGame> GetTeamWeek(
        HttpClient http,
        int season,
        int week,
        int team)
    {
        var season_week_team = new int[3] { season, week, team };
        var url = http.BaseAddress +
            ScheduledGamesRouteNames.controller_route + "/" +
            ScheduledGamesRouteNames.get_team_week;


        var response = await http.
            PostAsJsonAsync(url, season_week_team);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content
                .ReadFromJsonAsync<ScheduledGame>()
                ?? new();
        }

        return new ScheduledGame();
    }

    internal static async Task SaveNewScheduleRecord(
        HttpClient http,
        ScheduledGame game)
    {
        var url = http.BaseAddress +
            ScheduledGamesRouteNames.controller_route + "/" +
            ScheduledGamesRouteNames.save_new_record;

        var response = await http.PostAsJsonAsync(url, game);
        if (response.IsSuccessStatusCode)
        {
            await Task.CompletedTask;
        }

        await Task.CompletedTask;
    }
}
