﻿using FBTracker.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FBTracker.Client.Areas.Teams.TeamDetails;
public partial class TeamDetailsView : ComponentBase
{
    [Parameter]
    public Team Team { get; set; } = default!;

    [Parameter]
    public IEnumerable<ScheduledGame> Schedule { get; set; }
        = Enumerable.Empty<ScheduledGame>();

    [Parameter]
    public IEnumerable<Team> Teams { get; set; }
        = Enumerable.Empty<Team>();

    [Parameter]
    public bool ScheduleIsValidated { get; set; }

    [Parameter]
    public EventCallback<ScheduledGame> OnSubmitGameSchedule { get; set; }

    [Parameter]
    public EventCallback OnValidateSchedule { get; set; }

    [Parameter]
    public bool ScheduleIsValid { get; set; }

    private bool _showAddGameForm = false;
    private int?[] _unscheduledGames = new int?[18];
    private bool _scheduleFull = false;
    private bool _byeWeekExists = false;

    protected override async Task OnInitializedAsync()
    {
        _scheduleFull = ScheduleFull();
        //if (_scheduleFull) await ValidateSchedule();
    }

    private async Task ToggleShowAddGameForm()
    {
        _showAddGameForm = !_showAddGameForm;
        if (_showAddGameForm)
        {
            await PrepareAddGameFormData();
        }
    }

    private async Task ScheduleNewGame(ScheduledGame game)
    {
        _showAddGameForm = false;
        await OnSubmitGameSchedule.InvokeAsync(game);
    }

    private async Task PrepareAddGameFormData()
    {
        GetDefaultScheduleData();

        if (Schedule.Any())
        {
            CheckForByeWeek();
            foreach (var game in Schedule)
            {
                _unscheduledGames[game.Week - 1] = null;
            }

            //if (ScheduleFull())
            //{
            //    _scheduleFull = true;
            //    await ValidateSchedule();
            //}
        }
        
        await Task.CompletedTask;
    }

    private void GetDefaultScheduleData()
    {
        for (int i = 1; i < 19; i++)
        {
            _unscheduledGames[i - 1] = i;
        }
    }

    private void CheckForByeWeek()
    {
        foreach (var game in Schedule)
        {
            if (game.ByeTeamId == Team.Id)
            {
                _byeWeekExists = true;
            }
        }
    }

    private bool ScheduleFull()
    {
        var full = true;
        foreach (var game in _unscheduledGames)
        {
            if (game is not null) full = false;
        }

        return full;
    }

    private async Task ValidateSchedule()
    {
        await OnValidateSchedule.InvokeAsync();
    }
}
