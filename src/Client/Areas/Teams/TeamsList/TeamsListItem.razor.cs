using FBTracker.Shared.Models;
using FBTracker.Shared.Enums;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FBTracker.Client.Areas.Teams.TeamsList;
public partial class TeamsListItem : ComponentBase
{
    
    [Parameter]
    public Team Team { get; set; } = default!;

    [Parameter]
    public bool EnableEdit { get; set; } = false;

    [Parameter]
    public bool ShowEditForm { get; set; } = false;

    [Parameter]
    public EventCallback<Team> OnSubmitEdit { get; set; }

    [Parameter]
    public EventCallback<int> OnToggleShowEditForm { get; set; }

    [Parameter]
    public EventCallback<int> OnSelectTeamDetails { get; set; }

    private async Task ToggleShowEditForm()
    {
        await OnToggleShowEditForm.InvokeAsync(Team.Id);
    }

    private async Task SubmitEdit(Team team)
    {
        await OnSubmitEdit.InvokeAsync(team);
        await CancelEditTeam();
    }

    private async Task CancelEditTeam()
    {
        await OnToggleShowEditForm.InvokeAsync(-1);
    }

    private async Task SelectTeamDetails()
    {
        await OnSelectTeamDetails.InvokeAsync(Team.Id);
    }
}
