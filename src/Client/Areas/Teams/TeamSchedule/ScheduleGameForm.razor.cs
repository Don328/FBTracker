using FBTracker.Shared.Enums;
using FBTracker.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FBTracker.Client.Areas.Teams.TeamSchedule;
public partial class ScheduleGameForm : ComponentBase
{
    [Parameter]
    public EventCallback<ScheduledGame> OnSubmit { get; set; }

    [Parameter]
    public List<Team> Teams { get; set; }
        = default!;

    [Parameter]
    public Team SelectedTeam { get; set; }
        = new();

    [Parameter]
    public int?[] UnscheduledWeeks { get; set; }
        = new int?[18];

    [Parameter]
    public bool ByeWeekScheduled { get; set; }
        = false;

    private enum GameType
    {
        Bye,
        Home,
        Away
    }

    private GameType _gameType;
    private ScheduledGame _editModel = new();
    private List<string> _error_messages = new();

    private GameType SelectedGameType
    {
        get { return _gameType; }
        set
        {
            _gameType = value;
            switch (_gameType)
            {
                case GameType.Bye:
                    _editModel.GameDay = GameDay.Bye;
                    _editModel.ByeTeamId = SelectedTeam.Id;
                    _editModel.HomeTeamId = -1;
                    _editModel.AwayTeamId = -1;
                    break;
                case GameType.Home:
                    _editModel.ByeTeamId = -1;
                    _editModel.AwayTeamId = -1;
                    _editModel.HomeTeamId = SelectedTeam.Id;
                    break;
                case GameType.Away:
                    _editModel.ByeTeamId = -1;
                    _editModel.AwayTeamId = SelectedTeam.Id;
                    _editModel.HomeTeamId = -1;
                    break;
            }
        }
    }

    protected override async Task OnInitializedAsync()
    {
        Teams.Remove(SelectedTeam);
        await base.OnInitializedAsync();
    }

    private async Task Submit()
    {
        if (IsValid())
        {
            await OnSubmit.InvokeAsync(_editModel);
        }
    }

    private bool IsValid()
    {
        bool isValid = true;
        if (_editModel.Week < 1 ||
            _editModel.Week > 18)
        {
            _error_messages.Add("Week must be from 1 to 18");
            isValid = false;
        }


        switch (_editModel.ByeTeamId)
        {
            case > 0:
                if (ByeWeekScheduled)
                {
                    _error_messages.Add("Bye week already scheduled for this team.");
                    isValid = false;
                }

                if (_editModel.HomeTeamId > 0)
                {
                    _error_messages.Add("Bye should not be selected while home team is selected.");
                    isValid = false;
                }

                if (_editModel.AwayTeamId > 0)
                {
                    _error_messages.Add("Bye should not be selected while away team is selected.");
                    isValid = false;
                }
                if (_editModel.GameDay != GameDay.Bye)
                {
                    _error_messages.Add("Day of week should not be selected if bye week is selected.");
                    isValid = false;
                }
                break;
            case < 1:
                if (_editModel.HomeTeamId < 1)
                {
                    _error_messages.Add("Home team must be selected unless Bye is selected.");
                    isValid = false;
                }

                if (_editModel.AwayTeamId < 1)
                {
                    _error_messages.Add("Away team must be selected unless Bye is selected.");
                    isValid = false;
                }

                if (_editModel.GameDay == GameDay.Bye)
                {
                    _error_messages.Add("Day of week must be selected unless Bye is selected.");
                    isValid = false;
                }
                break;
        }

        return isValid;
    }
}
