using FBTracker.Shared.GloblaConstants;
using FBTracker.Shared.GloblaConstants.EndpointTags;
using FBTracker.Shared.Models;
using FBTracker.Shared.QueryObjects;
using FBTracker.Shared.QueryObjects.State;
using System.Data;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace FBTracker.Client.DataAccess;

internal static class StateAccess
{
    internal static async Task<int> GetSelectedSeason(
        HttpClient http)
    {
        var url = new QueryUrl(
            http.BaseAddress ?? new(""),
            StateRouteNames.controller_route,
            StateRouteNames.selected_season)
                .ToString();

        var query = new UserStateQuery()
        { Id = StateConstants.defaultUserId };

        var response = await http.PostAsJsonAsync(
            url, query);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content
                .ReadFromJsonAsync<int>();
        }

        return -1;
    }

    internal static async Task<bool> SetSelectedSeason(
        HttpClient http,
        UserStateQuery query)
    {
        var url = new QueryUrl(
            http.BaseAddress ?? new(""),
            StateRouteNames.controller_route,
            StateRouteNames.select_season)
                .ToString();

        var response = await http.PostAsJsonAsync(
            url, query);

        return await Task.FromResult(response.IsSuccessStatusCode);
    }

    internal static async Task<bool> CheckTeamsConfirmed(
        HttpClient http,
        UserStateQuery query)
    {
        var url = new QueryUrl(
            http.BaseAddress ?? new(""),
            StateRouteNames.controller_route,
            StateRouteNames.teams_confirmed)
                .ToString();

        var response = await http.PostAsJsonAsync(
            url, query);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content
                .ReadFromJsonAsync<bool>();
        }

        return false;
    }

    internal static async Task<bool> CheckScheduleConfirmed(
        HttpClient http,
        UserStateQuery query)
    {
        var url = new QueryUrl(
            http.BaseAddress ?? new(""),
            StateRouteNames.controller_route,
            StateRouteNames.schedule_confirmed)
                .ToString();

        var response = await http.PostAsJsonAsync(
            url, query);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content
                .ReadFromJsonAsync<bool>();
        }

        return false;
    }

    internal static async Task<bool> ConfirmTeamsList(
        HttpClient http,
        UserStateQuery query)
    {
        var url = new QueryUrl(
            http.BaseAddress ?? new(""),
            StateRouteNames.controller_route,
            StateRouteNames.confirm_teams_list)
                .ToString();

        var response = await http.PostAsJsonAsync(
            url, query);

        if (response.IsSuccessStatusCode)
        { return true; }
        return false;
    }
}
