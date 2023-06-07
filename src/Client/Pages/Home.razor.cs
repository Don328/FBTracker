using FBTracker.Client.DataAccess;
using FBTracker.Shared.HardValues.EndpointTags;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;

namespace FBTracker.Client.Pages;
public partial class Home : ComponentBase
{
    [Inject]
    public HttpClient Http { get; set; } = default!;

    private bool _seasonLoaded = false;

    protected override async Task OnInitializedAsync()
    {
        _seasonLoaded = await StateAccess
            .CheckSeasonLoaded(Http);
    }


}
