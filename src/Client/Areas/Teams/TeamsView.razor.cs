using FBTracker.Client.DataAccess;
using FBTracker.Shared.Models;
using FBTracker.Shared.QueryObjects.Games;
using FBTracker.Shared.QueryObjects.State;
using FBTracker.Shared.QueryObjects.Teams;
using Microsoft.AspNetCore.Components;

namespace FBTracker.Client.Areas.Teams;
public partial class TeamsView : ComponentBase
{
    [Inject]
    public ILogger<TeamsView> Logger { get; set; } = default!;

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
        Logger.LogInformation("Initializing");
        _season = await StateAccess
            .GetSelectedSeason(Http);

        Logger.LogInformation($"Selected season: {_season}");
        await GetTeams();
        await CheckTeamsConfirmed();
        await Task.CompletedTask;
    }

    private async Task CheckTeamsConfirmed()
    {
        var query = new UserStateQuery
        { SelectedSeason = _season };

        _teamsConfirmed = await StateAccess.CheckTeamsConfirmed(Http, query);
        Logger.LogInformation($"Teams confirmed: {_teamsConfirmed}");
        await Task.CompletedTask;
    }

    private async Task GetTeams()
    {
        Logger.LogInformation($"Getting teams for season: {_season}");
        var query = new TeamsQuery
        { Season = _season };

        _teams = Enumerable.Empty<Team>();
        _teams = await TeamsAccess.GetTeams(Http, query);
        Logger.LogInformation($"Teams loaded: { _teams.Any() }");
        Logger.LogInformation($"Teams count: {_teams.Count()}");

        await Task.CompletedTask;
    }

    private async Task UpdateTeamData(Team team)
    {
        Logger.LogInformation($"Updating data for team: {team.Id}, {team.Abrev}");
        var success = await TeamsAccess.UpdateTeamData(
            Http,
            team);

        if (success)
        {
            Logger.LogInformation("Update Successful");
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
        Logger.LogInformation($"Loading teams from the {_previousSeasonToLoad} season into {_season}");

        var query = new SeasonTeamTransfer
        {
            LoadFromSeason = _previousSeasonToLoad,
            LoadToSeason = _season
        };

        _teams = await TeamsAccess.LoadPreviousSeasonTeams(
            Http, query);

        if (_teams.Count() != 32)
        {
            Logger.LogInformation("Season load error");
        }
        else
        {
            Logger.LogInformation("Season transfer succsessful");
        }

    }

    private async Task ConfirmTeamsList()
    {
        Logger.LogInformation($"Confirming teams list for: {_season}");
        var query = new UserStateQuery
        { SelectedSeason = _season };

        await StateAccess
            .ConfirmTeamsList(Http, query);

        _teamsConfirmed = await StateAccess
            .CheckTeamsConfirmed(Http, query);

        if (_teamsConfirmed ) 
        {
            Logger.LogInformation("Teams confirmed");
        }
        else
        {
            Logger.LogInformation("An error occurred confirming the teams list");
        }
    }

    private async Task SubmitNewTeam(Team team)
    {
        Logger.LogInformation($"Submitting new team data for: {team.Locale}, {team.Name} [{team.Abrev}]");
        var success = await TeamsAccess
            .SubmitNewTeam(Http, team);

        if (success)
        {
            Logger.LogInformation("Team data submitted successfully");
            await CheckTeamsConfirmed();
        }
        else
        {
            Logger.LogInformation("An error ocurred while submitting team data");
        }
    }

    private async Task SelectTeamDetails(int teamId)
    {
        Logger.LogInformation($"Selecting team: {teamId} from teams list");
        _selectedTeam = (from t in _teams
                         where t.Id == teamId
                         select t).First();
        Logger.LogInformation($"Selected: {_selectedTeam.Id} [{_selectedTeam.Abrev}]");
        
        await GetSelectedTeamSchedule();
        StateHasChanged();
        await Task.CompletedTask;
    }

    private async Task GetSelectedTeamSchedule()
    {
        Logger.LogInformation($"Getting Schedule data for: {_selectedTeam.Abrev}");
        var query = new GamesQuery
        {
            Season = _season,
            TeamId = _selectedTeam.Id
        };

        _selectedTeamSchedule = await GamesAccess
            .GetTeamSeason(Http, query);
    }

    private async Task SubmitNewScheduledGame(ScheduledGame game)
    {
        Logger.LogInformation($"Submitting new scheduled game");
        int selectedTeamId = _selectedTeam.Id;
        game.Season = _season;
        await GamesAccess.SaveNewScheduleRecord(Http, game);
        _selectedTeam = new();
        await SelectTeamDetails(selectedTeamId);
        await Task.CompletedTask;
    }
}
