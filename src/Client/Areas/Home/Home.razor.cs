using Blazored.Modal.Services;
using FBTracker.Client.DataAccess;
using FBTracker.Shared.GloblaConstants.EndpointTags;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using FBTracker.Client.Areas.ModalForms.SeasonSelect;
using FBTracker.Shared.QueryObjects.State;

namespace FBTracker.Client.Areas.Home;
public partial class Home : ComponentBase
{
    private int _season = 0;
    private bool _teamsConfirmed = false;
    private bool _schedulesConfirmed = false;

    [Inject]
    public ILogger<Home> Logger { get; set; } = default!;

    [Inject]
    public NavigationManager NavMan { get; set; } = default!;

    [Inject]
    public HttpClient Http { get; set; } = default!;

    [CascadingParameter]
    public IModalService Modal { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        Logger.LogInformation("Initializing");

        _season = await StateAccess
            .GetSelectedSeason(Http);

        Logger.LogInformation($"Selected season: {_season}");

        if (_season > 0)
        {
            var query = new UserStateQuery
            { SelectedSeason = _season };

            _teamsConfirmed = await StateAccess
                .CheckTeamsConfirmed(Http, query);

            Logger.LogInformation($"Teams confirmed: {_teamsConfirmed}");
            
            if (_teamsConfirmed)
            {
                _schedulesConfirmed = await StateAccess
                    .CheckScheduleConfirmed(Http, query);

                Logger.LogInformation($"Schedules confirmed: {_schedulesConfirmed}");
            }
        }
        else
        {
            Logger.LogError("Season not set");
        }

    }

    private async Task ShowChangeSeasonForm()
    {
        Logger.LogInformation("SeasonSelect form selected");
        var season = await SeasonSelectService.Show(Modal);
        if (season is not null)
        {
            var query = new UserStateQuery
            { SelectedSeason = (int)season };
            await StateAccess.SetSelectedSeason(Http, query);
            Logger.LogInformation($"Season selected: {season}");
            NavMan.NavigateTo("/", true);
        }

        await Task.CompletedTask;
    }
}
