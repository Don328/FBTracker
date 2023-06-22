using FBTracker.Client.DataAccess;
using FBTracker.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace FBTracker.Client.Pages;
public partial class TeamsView : ComponentBase
{
    [Inject]
    public HttpClient Http { get; set; } = default!;

    private const int _defaultSeasonToLoad = 2021;

    private int _season;
    private IEnumerable<Team> _teams = Enumerable.Empty<Team>();
    private bool TeamsLoaded => _teams.Any();
    private int _previousSeasonToLoad = _defaultSeasonToLoad;
    private bool _teamsConfirmed = false;
    private bool _showLoadPreviousTeams = false;

    private Team _selectedTeam = new();
    private IEnumerable<ScheduledGame> _selectedTeamSchedule
        = Enumerable.Empty<ScheduledGame>();

    protected override async Task OnInitializedAsync()
    {
        _season = await StateAccess
            .GetSelectedSeason(Http);
        _teams = await TeamsAccess.GetTeams(Http, _season);
        _teamsConfirmed = await StateAccess.CheckTeamsConfirmed(Http, _season);

        
        CheckTeamsConfirmed();

        await Task.CompletedTask;
    }

    private async void CheckTeamsConfirmed()
    {
        _teamsConfirmed = await StateAccess.CheckTeamsConfirmed(Http, _season);
    }

    private async Task GetTeams()
    {
        _teams = Enumerable.Empty<Team>();
        _teams = await TeamsAccess.GetTeams(Http, _season);
        
        await Task.CompletedTask;
    }

    private async Task UpdateTeamData(Team team)
    {
        var success = await TeamsAccess.UpdateTeamData(
            Http,
            team);

        if(success)
        {
            await GetTeams();
        }

        await Task.CompletedTask;
    }

    private void ToggleShowLoadPreviousTeams()
    {
        _showLoadPreviousTeams = !_showLoadPreviousTeams;
    }

    private async Task LoadPreviousSeasonTeams()
    {
        var season = await StateAccess.GetSelectedSeason(Http);
        _teams = await TeamsAccess.LoadPreviousSeasonTeams(
            Http, 
            fromSeason: _previousSeasonToLoad,
            toSeason: season);
    }

    private async Task ConfirmTeamsList()
    {
        await StateAccess
            .ConfirmTeamsList(Http, _season);
        
        _teamsConfirmed = await StateAccess
            .CheckTeamsConfirmed(Http, _season);
    }

    private async Task SubmitNewTeam(Team team)
    {
        var success = await TeamsAccess
            .SubmitNewTeam(Http, team);

        if (success)
        {
            CheckTeamsConfirmed();
        }
    }

    private async Task SelectTeamDetails(int teamId)
    {
        _selectedTeam = (from t in _teams
                         where t.Id == teamId
                         select t).First();

        await GetSelectedTeamSchedule();
        StateHasChanged();
        await Task.CompletedTask;
    }

    private async Task GetSelectedTeamSchedule()
    {
        var season = await StateAccess.GetSelectedSeason(Http);

        _selectedTeamSchedule = await GamesAccess
            .GetTeamSeason(
                http: Http,
                season: season,
                teamId: _selectedTeam.Id);
    }

    private async Task SubmitNewScheduledGame(ScheduledGame game)
    {
        game.Season = _season;
        await GamesAccess.SaveNewScheduleRecord(Http, game);
        await Task.CompletedTask;
    }
}
