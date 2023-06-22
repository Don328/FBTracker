using FBTracker.Shared.GloblaConstants.EndpointTags;
using FBTracker.Shared.Models;
using System.Net.Http.Json;

namespace FBTracker.Client.DataAccess;

internal static class TeamsAccess
{
    internal static async Task<IEnumerable<Team>> GetTeams(
        HttpClient http,
        int season)
    {
        var response = await http.PostAsJsonAsync(
            http.BaseAddress +
            TeamsRouteNames.controller_route + "/" +
            TeamsRouteNames.get,
            season);

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
        int fromSeason,
        int toSeason)
    {
        var response = await http.PostAsJsonAsync<(int, int)>(
            http.BaseAddress +
            TeamsRouteNames.controller_route + "/" +
            TeamsRouteNames.load_from_season,
             (fromSeason, toSeason));

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
        var response = await http.PostAsJsonAsync(
            http.BaseAddress +
            TeamsRouteNames.controller_route + "/" +
            TeamsRouteNames.update,
            team);

        if (response.IsSuccessStatusCode)
        { return true; }
        return false;
    }

    internal static async Task<bool> SubmitNewTeam(
        HttpClient http,
        Team team)
    {
        var response = await http.PostAsJsonAsync(
            http.BaseAddress +
            TeamsRouteNames.controller_route + "/" +
            TeamsRouteNames.add,
            team);

        if (response.IsSuccessStatusCode)
        { return true; }
        return false;
    }
}
