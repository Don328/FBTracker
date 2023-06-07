using FBTracker.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FBTracker.Client.Components.TeamsList;
public partial class TeamsList : ComponentBase
{
    [Parameter]
    public IEnumerable<Team> Teams { get; set; } = default!;

    [Parameter]
    public bool CanEdit { get; set; } = false;

    [Parameter]
    public EventCallback<Team> OnUpdateTeamData { get; set; }

    private int teamToEditId = -1;

    private void SelectTeamToEdit(int id)
    {
        teamToEditId = id;
        
        // Might need StateHasChanged() here?
    }

    private async Task UpdateTeamData(Team data)
    {
        await OnUpdateTeamData.InvokeAsync(data);
    }
}
