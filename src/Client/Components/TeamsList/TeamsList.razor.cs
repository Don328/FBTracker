using FBTracker.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FBTracker.Client.Components.TeamsList;
public partial class TeamsList : ComponentBase
{
    [Parameter]
    [EditorRequired]
    public IEnumerable<Team> Teams { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public bool CanEdit { get; set; } = false;

    [Parameter]
    public EventCallback<Team> OnUpdateTeamData { get; set; }

    [Parameter]
    public EventCallback<Team> OnSubmitNewTeam { get; set; }

    [Parameter]
    public EventCallback OnConfirm { get; set; }

    [Parameter]
    public EventCallback<int> OnShowTeamDetails { get; set; }

    private int _teamToEditId = -1;
    private bool _locale_reverse = false;
    private bool _name_reverse = false;
    private bool _abrev_reverse = false;
    private bool _division_reverse = false;

    private void SelectTeamToEdit(int id)
    {
        _teamToEditId = id;
    }

    private async Task UpdateTeamData(Team team)
    {
        await OnUpdateTeamData.InvokeAsync(team);
    }

    private async Task SubmitNewTeamData(Team team)
    {
        await OnSubmitNewTeam.InvokeAsync(team);
    }

    private async Task Confirm()
    {
        await OnConfirm.InvokeAsync();
    }

    private void SortByLocale()
    {
        _locale_reverse = !_locale_reverse;
        if (_locale_reverse)
        {
            Teams = Teams.OrderByDescending(t => t.Locale);
            return;
        }

        Teams = Teams.OrderBy(t => t.Locale);
    }

    private void SortByName()
    {
        _name_reverse = !_name_reverse;
        if (_name_reverse)
        {
            Teams = Teams.OrderByDescending(t => t.Name);
            return;
        }

        Teams = Teams.OrderBy(t => t.Name);
    }

    private void SortByAbrev()
    {
        _abrev_reverse = !_abrev_reverse;
        if (_abrev_reverse)
        {
            Teams = Teams.OrderByDescending(t => t.Abrev);
            return;
        }

        Teams = Teams.OrderBy(t => t.Abrev);
    }

    private void SortByDivision()
    {
        _division_reverse = !_division_reverse;
        if (_division_reverse)
        {
            Teams = Teams.OrderByDescending(t => t.Conference)
                .ThenByDescending(t => t.Region);
            return;
        }

        Teams = Teams.OrderBy(t => t.Conference)
            .ThenBy(t => t.Region);
    }

    private Task ShowTeamDetails(int teamId)
    {
        return OnShowTeamDetails.InvokeAsync(teamId);
    }
}
