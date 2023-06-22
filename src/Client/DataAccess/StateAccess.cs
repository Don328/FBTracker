using FBTracker.Shared.GloblaConstants;
using FBTracker.Shared.GloblaConstants.EndpointTags;
using FBTracker.Shared.Models;
using System.Data;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace FBTracker.Client.DataAccess;

internal static class StateAccess
{
    internal static async Task<int> GetSelectedSeason(
        HttpClient http)
    {
        var response = await http.PostAsJsonAsync(
            http.BaseAddress +
            StateRouteNames.controller_route + "/" +
            StateRouteNames.selected_season,
            StateConstants.defaultUserId);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content
                .ReadFromJsonAsync<int>();
        }

        return -1;
    }

    internal static async Task<bool> SetSelectedSeason(
        HttpClient http,
        int season)
    {
        var response = await http.PostAsJsonAsync(
            http.BaseAddress +
            StateRouteNames.controller_route + "/" +
            StateRouteNames.select_season,
            new Tuple<int, int>(
                StateConstants.defaultUserId,
                season));

        return await Task.FromResult(response.IsSuccessStatusCode);
    }

    internal static async Task<bool> CheckTeamsConfirmed(
        HttpClient http,
        int season)
    {
        var response = await http.PostAsJsonAsync(
            http.BaseAddress +
            StateRouteNames.controller_route + "/" +
            StateRouteNames.teams_confirmed,
            season);

        if (response.IsSuccessStatusCode) 
        {
            return await response.Content
                .ReadFromJsonAsync<bool>();
        }

        return false;
    }

    internal static async Task<bool> CheckScheduleConfirmed(
        HttpClient http,
        int season)
    {
        var response = await http.PostAsJsonAsync(
            http.BaseAddress +
            StateRouteNames.controller_route + "/" +
            StateRouteNames.schedule_confirmed,
            season);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content
                .ReadFromJsonAsync<bool>();
        }

        return false;
    }

    internal static async Task<bool> ConfirmTeamsList(
        HttpClient http,
        int season)
    {
        var response = await http.PostAsJsonAsync(
            http.BaseAddress +
            StateRouteNames.controller_route + "/" +
            StateRouteNames.confirm_teams_list,
            season);

        if (response.IsSuccessStatusCode) 
        { return true; } return false;
    }
}
