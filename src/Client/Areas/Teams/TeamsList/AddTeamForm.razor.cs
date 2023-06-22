using FBTracker.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FBTracker.Client.Areas.Teams.TeamsList;
public partial class AddTeamForm : ComponentBase
{
    [Parameter]
    public EventCallback<Team> SubmitNewTeamData { get; set; }

    [Parameter]
    public Team InitData { get; set; } = new();

    private Team _editModel = new();


    protected override async Task OnInitializedAsync()
    {
        InitializeEditModel();
        await base.OnInitializedAsync();
    }

    private void InitializeEditModel()
    {
        _editModel.Id = InitData.Id;
        _editModel.Season = InitData.Season;
        _editModel.Locale = InitData.Locale;
        _editModel.Name = InitData.Name;
        _editModel.Abrev = InitData.Abrev;
        _editModel.Conference = InitData.Conference;
        _editModel.Region = InitData.Region;
    }

    private void ClearForm()
    {
        if (InitData.Id > 0)
        {
            InitializeEditModel();
        }
        else
        {
            _editModel = new();
        }


    }

    private async Task SubmitNewTeam()
    {
        // Add validation

        await SubmitNewTeamData.InvokeAsync(_editModel);
    }
}
