using Blazored.Modal.Services;
using FBTracker.Client.DataAccess;
using FBTracker.Client.Modals;
using FBTracker.Shared.GloblaConstants;
using FBTracker.Shared.GloblaConstants.EndpointTags;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace FBTracker.Client;

public partial class NavMenu : ComponentBase
{
    private int _selectedSeason = 0;

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
        var season = await SeasonSelectService.Show(Modal);
        if (season is not null)
        {
            await SetSelectedSeason((int)season);
            NavMan.NavigateTo("/", true);
        }

        await Task.CompletedTask;
    }

    private async Task SetSelectedSeason(int season)
    {
        await StateAccess.SetSelectedSeason(Http, season);
        _selectedSeason = await StateAccess.GetSelectedSeason(Http);
        await Task.CompletedTask;
    }
}
