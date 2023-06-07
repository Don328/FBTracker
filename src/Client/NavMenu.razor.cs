using Blazored.Modal.Services;
using FBTracker.Client.DataAccess;
using FBTracker.Client.Modals;
using FBTracker.Shared.HardValues;
using FBTracker.Shared.HardValues.EndpointTags;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace FBTracker.Client;

public partial class NavMenu : ComponentBase
{
    private bool _seasonSelected = default!;
    private int _selectedSeason = default!;

    [Inject]
    public HttpClient Http { get; set; } = default!;

    [CascadingParameter]
    public IModalService Modal { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _seasonSelected = await StateAccess
            .CheckSeasonLoaded(Http);
        
        if (_seasonSelected)
        {
            await StateAccess.GetSelectedSeason(Http);
        }

        await Task.CompletedTask;
    }

    private async Task OnSelectSeason()
    {
        var seasonSelect = Modal.Show<SeasonSelect>();
        var result = await seasonSelect.Result;

        if (result.Confirmed && 
            result.Data is not null)
        {
            var season = Int32.Parse(result.Data.ToString()??"");
            if (season > StateConstants.seasonMin &&
                season < StateConstants.seasonMax)
            {
                await SetSelectedSeason(season);
            }
        }

        await Task.CompletedTask;
    }

    private async Task SetSelectedSeason(int season)
    {
        var success = await StateAccess.SetSelectedSeason(Http, season);
        if (success)
        {
            _seasonSelected = await StateAccess.CheckSeasonLoaded(Http);
            if (_seasonSelected)
            {
                _selectedSeason = await StateAccess.GetSelectedSeason(Http);
            }
        }

        await Task.CompletedTask;
    }
}
