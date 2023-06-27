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
    private bool _selectedTeamScheduleIsValid = false;

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
        if (_selectedTeamSchedule.Count() == 18)
        {
            await ValidateTeamSchedule();  
        }
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
        Logger.LogInformation($"Teams loaded: {_teams.Any()}");
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

        if (_teamsConfirmed)
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
        //StateHasChanged();
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

    private async Task ValidateTeamSchedule()
    {
        Logger.LogInformation($"Validating Schedule for team [id: {_selectedTeam.Id}]");
        await GetSelectedTeamSchedule();
        if (ValidateDivisionGames())
        {
            if(ValidateHome_Away_Bye_Games())
            {
                //if (ValidateConferenceGames())
                //{
                    _selectedTeamScheduleIsValid = true;
                    await Task.CompletedTask;
                //}
            }
        }
        else
        {
            _selectedTeamScheduleIsValid = false;
            await Task.CompletedTask;
        }
    }

    private bool ValidateDivisionGames()
    {
        var rivals = GetDivisionRivals();

        bool isValid = true;
        int home_0 = 0;
        int away_0 = 0;
        int home_1 = 0;
        int away_1 = 0;
        int home_2 = 0;
        int away_2 = 0;

        foreach (var game in _selectedTeamSchedule)
        {
            if (game.HomeTeamId == _selectedTeam.Id)
            {
                if (rivals[0] == game.AwayTeamId)
                    away_0++;
                if (rivals[1] == game.AwayTeamId)
                    away_1++;
                if (rivals[2] == game.AwayTeamId)
                    away_2++;
            }

            if (game.AwayTeamId == _selectedTeam.Id)
            {
                if (rivals[0] == game.HomeTeamId)
                    home_0++;
                if (rivals[1] == game.HomeTeamId)
                    home_1++;
                if (rivals[2] == game.HomeTeamId)
                    home_2++;
            }

        }

        if (home_0 != 1) isValid = false;
        if (home_1 != 1) isValid = false;
        if (home_2 != 1) isValid = false;
        if (away_0 != 1) isValid = false;
        if (away_1 != 1) isValid = false;
        if (away_2 != 1) isValid = false;

        return isValid;
    }

    private bool ValidateHome_Away_Bye_Games()
    {
        bool isValid = true;
        int homeGames = 0;
        int awayGames = 0;
        int byeGames = 0;
        foreach (var game in _selectedTeamSchedule)
        {
            if (game.HomeTeamId == _selectedTeam.Id)
                homeGames++;

            if (game.AwayTeamId == _selectedTeam.Id)
                awayGames++;

            if (game.ByeTeamId == _selectedTeam.Id)
                byeGames++;
        }

        if (homeGames < 8) isValid = false;
        if (awayGames < 8) isValid = false;
        if (byeGames != 1) isValid = false;
        return isValid;
    }

    private int[] GetDivisionRivals()
    {
        var rivals = new int[3];

        var i = 0;
        foreach(var team in _teams) 
        {
            if (i > 2) break;
            if (team.Id == _selectedTeam.Id) continue;
            if (team.Conference != _selectedTeam.Conference) continue;
            if (team.Region != _selectedTeam.Region) continue;  
                
            rivals[i] = team.Id;
            i++;
        }

        return rivals;
    }

    private bool ValidateConferenceGames()
    {
        bool isValid = true;
        int conferenceGames = 0;
        int nonConferenceGames = 0;
        int[] conferenceTeams = new int[16];
        int[] nonConferenceTeams = new int[15];
        int conferenceTeamCount = 0;
        int nonConferenceTeamCount = 0;

        foreach(var team in _teams)
        {
            if (team.Id == _selectedTeam.Id) continue;
            
            if (team.Conference == _selectedTeam.Conference)
            {
                conferenceTeams[conferenceTeamCount] = team.Id;
                conferenceTeamCount++; 
            }

            if(team.Conference  != _selectedTeam.Conference)  
            {
                nonConferenceTeams[nonConferenceTeamCount] = team.Id;
                nonConferenceTeamCount++;
            }
        }

        foreach(var game in _selectedTeamSchedule) 
        {
            foreach (var id in conferenceTeams)
            {
                if (game.AwayTeamId == id ||
                    game.HomeTeamId == id)
                    conferenceGames++;
            }

            foreach (var id in nonConferenceTeams)
            {
                if (game.AwayTeamId == id ||
                    game.HomeTeamId == id)
                    nonConferenceGames++;
            }
        }

        if (conferenceGames < 11) isValid = false;
        if (conferenceGames > 13) isValid = false; ;
        if (nonConferenceGames < 4) isValid = false;
        if (nonConferenceGames > 6) isValid = false;

        return isValid;
    }
}
