using FBTracker.Shared.Models;
using FBTracker.Shared.Enums;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FBTracker.Client.Components.TeamsList;
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

    private Team _editModel = new();
    private bool _showNameInput = false;
    private bool _showLocaleInput = false;
    private bool _showAbrevInput = false;
    private bool _showConferenceInput = false;
    private bool _showRegionInput = false;

    protected override void OnInitialized()
    {
        NormalizeEditModel();
    }

    private void NormalizeEditModel()
    {
        _editModel.Name = Team.Name.ToString();
        _editModel.Locale = Team.Locale.ToString();
        _editModel.Abrev = Team.Abrev.ToString();
        _editModel.Conference = Team.Conference;
        _editModel.Region = Team.Region;

        _showNameInput = false;
        _showLocaleInput = false;
        _showAbrevInput = false;
        _showConferenceInput = false;
        _showRegionInput = false;
    }

    private async Task ToggleShowEditForm()
    {
        await OnToggleShowEditForm.InvokeAsync(Team.Id);
    }

    private async Task SubmitEdit()
    {
        await OnSubmitEdit.InvokeAsync(_editModel);
    }

    private async Task CancelEditTeam()
    {
        NormalizeEditModel();
        await OnToggleShowEditForm.InvokeAsync(-1);
    }

    private void ToggleLocaleInput()
    {
        if (_showLocaleInput) 
            _editModel.Locale = Team.Locale.ToString();

        _showLocaleInput = !_showLocaleInput;
    }

    private void ToggleNameInput()
    {
        if (_showNameInput)
            _editModel.Name = Team.Name.ToString();

        _showNameInput = !_showNameInput;
    }

    private void ToggleAbrevInput()
    {
        if (_showAbrevInput)
            _editModel.Abrev = Team.Abrev.ToString();

        _showAbrevInput = !_showAbrevInput;
    }

    private void ToggleConferenceInput()
    {
        if (_showConferenceInput)
            _editModel.Conference = Team.Conference;

        _showConferenceInput = !_showConferenceInput;
    }

    private void ToggleRegionInput()
    {
        if (_showRegionInput)
            _editModel.Region = Team.Region;

        _showRegionInput = !_showRegionInput;
    }
}
