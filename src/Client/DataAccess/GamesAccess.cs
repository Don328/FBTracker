using FBTracker.Shared.GloblaConstants.EndpointTags;
using FBTracker.Shared.Models;
using FBTracker.Shared.QueryObjects;
using FBTracker.Shared.QueryObjects.Games;
using System.Net.Http.Json;

namespace FBTracker.Client.DataAccess;

internal static class GamesAccess
{
    internal static async Task<IEnumerable<ScheduledGame>> GetSeasonSchedule(
        HttpClient http,
        GamesQuery query)
    {
        var url = new QueryUrl(
            http.BaseAddress?? new(""),
            ScheduledGamesRouteNames.controller_route,
            ScheduledGamesRouteNames.get_season)
            .ToString();

        var response = await http
            .PostAsJsonAsync(url, query);

        if (response.IsSuccessStatusCode)
        {
            return (IEnumerable<ScheduledGame>)response.Content;
        }

        return Enumerable.Empty<ScheduledGame>();
    }

    internal static async Task<IEnumerable<ScheduledGame>> GetWeekSchedule(
        HttpClient http,
        GamesQuery query)
    {
        var url = new QueryUrl(
            http.BaseAddress?? new(""),
            ScheduledGamesRouteNames.controller_route,
            ScheduledGamesRouteNames.get_week)
            .ToString();

        var response = await http
            .PostAsJsonAsync(
            url, query);
        
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
        GamesQuery query)
    {
        var url = new QueryUrl(
            http.BaseAddress?? new(""),
            ScheduledGamesRouteNames.controller_route,
            ScheduledGamesRouteNames.get_team_season)
            .ToString();

        var response = await http
            .PostAsJsonAsync(
            url, query);

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
        GamesQuery query)
    {
        var url = new QueryUrl(
            http.BaseAddress ?? new(""),
            ScheduledGamesRouteNames.controller_route,
            ScheduledGamesRouteNames.get_team_week)
            .ToString();

        var response = await http.
            PostAsJsonAsync(url, query);

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
        var url = new QueryUrl(
            http.BaseAddress?? new(""),
            ScheduledGamesRouteNames.controller_route,
            ScheduledGamesRouteNames.save_new_record)
            .ToString();
            
        var response = await http
            .PostAsJsonAsync(url, game);

        if (response.IsSuccessStatusCode)
        {
            await Task.CompletedTask;
        }

        await Task.CompletedTask;
    }
}
