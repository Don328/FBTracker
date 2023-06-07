using FBTracker.Shared.HardValues.EndpointTags;
using FBTracker.Shared.Models;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace FBTracker.Client.DataAccess;

public static class StateAccess
{
    public static async Task<bool> CheckSeasonLoaded(HttpClient http)
    {
        var url = http.BaseAddress +
            StateRouteNames.controller_route + "/" +
            StateRouteNames.seasonSelected;

        var seasonSelected = await http.GetFromJsonAsync<bool>(url);

        return await Task.FromResult(seasonSelected);
    }

    public static async Task<int> GetSelectedSeason(HttpClient http)
    {
        var url = http.BaseAddress +
    StateRouteNames.controller_route + "/" +
    StateRouteNames.season;

        var response = await http.GetFromJsonAsync(url, typeof(int));
        if (response is not null)
        {
            var success = Int32.TryParse(
                response.ToString(),
                out int result);

            if (success)
                return await Task
                    .FromResult(result);
        }

        return -1;
    }

    public static async Task<bool> SetSelectedSeason(HttpClient http, int season)
    {
        var url = http.BaseAddress +
    StateRouteNames.controller_route + "/" +
    StateRouteNames.season;

        var response = await http.PostAsJsonAsync(url, season);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }

    public static async Task<bool> CheckTeamsLoaded(HttpClient http)
    {
        var response = await http.GetAsync(
            http.BaseAddress +
            StateRouteNames.controller_route + "/" +
            StateRouteNames.teamsLoaded);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content
                .ReadFromJsonAsync<bool>();

            return result;
        }

        return false;
    }

    public static async Task<IEnumerable<Team>> GetLoadedTeams(HttpClient http)
    {
        var response = await http.GetAsync(
            http.BaseAddress +
            StateRouteNames.controller_route + "/" +
            StateRouteNames.teams);

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
}
