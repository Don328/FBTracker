using Blazored.Modal.Services;
using FBTracker.Client.DataAccess;
using FBTracker.Shared.GloblaConstants.EndpointTags;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using FBTracker.Client.Areas.ModalForms.SeasonSelect;

namespace FBTracker.Client.Areas.Home;
public partial class Home : ComponentBase
{
    private int _season = 0;
    private bool _teamsConfirmed = false;
    private bool _schedulesConfirmed = false;

    [Inject]
    public NavigationManager NavMan { get; set; } = default!;

    [Inject]
    public HttpClient Http { get; set; } = default!;

    [CascadingParameter]
    public IModalService Modal { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _season = await StateAccess
            .GetSelectedSeason(Http);

        if (_season > 0)
        {
            _teamsConfirmed = await StateAccess
                .CheckTeamsConfirmed(Http, _season);
            
            if (_teamsConfirmed)
            {
                _schedulesConfirmed = await StateAccess
                    .CheckScheduleConfirmed(Http, _season);
            }
        }
    }

    private async Task ShowChangeSeasonForm()
    {
        var season = await SeasonSelectService.Show(Modal);
        if (season is not null)
        {
            await StateAccess.SetSelectedSeason(Http, (int)season);
            NavMan.NavigateTo("/", true);
        }

        await Task.CompletedTask;
    }
}
