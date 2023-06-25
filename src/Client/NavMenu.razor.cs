using Blazored.Modal.Services;
using FBTracker.Client.DataAccess;
using FBTracker.Client.Areas.ModalForms.SeasonSelect;
using FBTracker.Shared.GloblaConstants;
using FBTracker.Shared.GloblaConstants.EndpointTags;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using FBTracker.Shared.QueryObjects.State;

namespace FBTracker.Client;

public partial class NavMenu : ComponentBase
{
    private int _selectedSeason = 0;

    [Inject]
    public ILogger<NavMenu> Logger { get; set; } = default!;

    [Inject]
    public HttpClient Http { get; set; } = default!;

    [Inject]
    public NavigationManager NavMan { get; set; } = default!;

    [CascadingParameter]
    public IModalService Modal { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _selectedSeason = await StateAccess
            .GetSelectedSeason(Http);
        await Task.CompletedTask;
    }

    private async Task OnSelectSeason()
    {
        Logger.LogInformation("SeasonSelect form selected");
        var season = await SeasonSelectService.Show(Modal);
        if (season is not null)
        {
            await SetSelectedSeason((int)season);
            Logger.LogInformation($"Season selected: {season}");
            NavMan.NavigateTo("/", true);
        }

        await Task.CompletedTask;
    }

    private async Task SetSelectedSeason(int season)
    {
        var query = new UserStateQuery 
        { SelectedSeason = season};
        Logger.LogInformation($"Setting selected season season: {season}");
        await StateAccess.SetSelectedSeason(Http, query);
        _selectedSeason = await StateAccess.GetSelectedSeason(Http);
        Logger.LogInformation($"Season set: {_selectedSeason}");
        await Task.CompletedTask;
    }
}
