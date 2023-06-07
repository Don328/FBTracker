using FBTracker.Shared.HardValues;
using FBTracker.Shared.Models;

namespace FBTracker.Server.State;

public class AppState
{
    public int Season { get; private set; }
    public IEnumerable<Team> Teams { get; private set; } = default!;

    public bool SeasonIsSet() =>
        Season > StateConstants.seasonMin &&
        Season < StateConstants.seasonMax;
    
    public bool TeamsLoaded() => Teams.Any();

    public void SetSeason(int season)
    {
        if (season > StateConstants.seasonMin &&
            season < StateConstants.seasonMax)
        Season = season;
    }

    public void LoadTeams(IEnumerable<Team> teams)
    {
        Teams = teams;
    }
}
