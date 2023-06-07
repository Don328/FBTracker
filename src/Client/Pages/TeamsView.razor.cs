using FBTracker.Client.DataAccess;
using FBTracker.Shared.HardValues.EndpointTags;
using FBTracker.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;

namespace FBTracker.Client.Pages;
public partial class TeamsView : ComponentBase
{
    [Inject]
    public HttpClient Http { get; set; } = default!;

    private IEnumerable<Team> _teams = Enumerable.Empty<Team>();
    private bool _teamsLoaded  => _teams.Any(); 

    protected override async Task OnInitializedAsync()
    {
        if (await CheckTeamsLoaded())
        {
            await GetTeams();
        }

        await Task.CompletedTask;
    }

    private async Task<bool> CheckTeamsLoaded()
    {
        return await StateAccess.CheckTeamsLoaded(Http); ;
    }

    private async Task GetTeams()
    {
        var teams = await StateAccess.GetLoadedTeams(Http);
        if (teams.Any())
        {
            _teams = teams;
        }

        await Task.CompletedTask;
    }

    private async Task UpdateTeamData(Team data)
    {
        await Task.CompletedTask;
    }
}
