using FBTracker.Shared.GloblaConstants.EndpointTags;
using FBTracker.Shared.Models;
using FBTracker.Shared.QueryObjects;
using FBTracker.Shared.QueryObjects.Teams;
using System.Net.Http.Json;

namespace FBTracker.Client.DataAccess;

internal static class TeamsAccess
{
    internal static async Task<IEnumerable<Team>> GetTeams(
        HttpClient http,
        TeamsQuery query)
    {
        var url = new QueryUrl(
            http.BaseAddress?? new(""),
            TeamsRouteNames.controller_route,
            TeamsRouteNames.get)
                .ToString();

        var response = await http.PostAsJsonAsync(
            url, query);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content
                .ReadFromJsonAsync<IEnumerable<Team>>();

            if (result is not null)
            {
                return (IEnumerable<Team>)result;
            }
        }

        return Enumerable.Empty<Team>();
    }

    internal static async Task<IEnumerable<Team>> LoadPreviousSeasonTeams(
        HttpClient http,
        SeasonTeamTransfer query)
    {
        var url = new QueryUrl(
            http.BaseAddress ?? new(""),
            TeamsRouteNames.controller_route,
            TeamsRouteNames.load_from_season)
                .ToString();

        var response = await http.PostAsJsonAsync(
             url, query);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<Team>>();
            if (result is not null)
            {
                return result;
            }
        }

        return Enumerable.Empty<Team>();
    }

    internal static async Task<bool> UpdateTeamData(
        HttpClient http,
        Team team)
    {
        var url = new QueryUrl(
            http.BaseAddress?? new(""),
            TeamsRouteNames.controller_route,
            TeamsRouteNames.update)
                .ToString();

        var response = await http.PostAsJsonAsync(
            url, team);

        if (response.IsSuccessStatusCode)
        { return true; }
        return false;
    }

    internal static async Task<bool> SubmitNewTeam(
        HttpClient http,
        Team team)
    {
        var url = new QueryUrl(
            http.BaseAddress?? new(""),
            TeamsRouteNames.controller_route,
            TeamsRouteNames.add)
                .ToString();

        var response = await http.PostAsJsonAsync(
            url, team);

        if (response.IsSuccessStatusCode)
        { return true; }
        return false;
    }
}
