using FBTracker.Server.Data.Schema.Constants;

namespace FBTracker.Server.Data.Schema.Commands;

internal static class ReadRow
{
    internal const string userState_by_id =
        "Select " +
        $"{PropertyNames.id}, " +
        $"{PropertyNames.season} " +
        "FROM " +
        $"{TableNames.userState} " +
        "WHERE " +
        $"{PropertyNames.id}=" +
        $"{ParameterNames.id};";

    internal const string team_by_id =
        "SELECT " +
            $"{PropertyNames.id}, " +
            $"{PropertyNames.season}, " +
            $"{PropertyNames.name}, " +
            $"{PropertyNames.locale}, " +
            $"{PropertyNames.abrev}, " +
            $"{PropertyNames.conference}, " +
            $"{PropertyNames.region} " +
        "FROM " +
            $"{TableNames.teams} " +
        "WHERE " +
            $"{PropertyNames.id}=" +
            $"{ParameterNames.id};";

    internal const string seasonPrep_bySeason =
        "SELECT " +
            $"{PropertyNames.id}, " +
            $"{PropertyNames.season}, " +
            $"{PropertyNames.teamsConfirmed}, " +
            $"{PropertyNames.schedulesLoaded} " +
        "FROM " +
            $"{TableNames.seasonPrep} " +
        "WHERE " +
            $"{PropertyNames.season}=" +
            $"{ParameterNames.season};";

    internal const string scheduledGames_byes_byWeek =
        "SELECT " +
        $"{PropertyNames.id}, " +
        $"{PropertyNames.season}, " +
        $"{PropertyNames.week}, " +
        $"{PropertyNames.homeTeamId}, " +
        $"{PropertyNames.awayTeamId}, " +
        $"{PropertyNames.byeTeamId}, " +
        $"{PropertyNames.dayOfWeekIdx} " +
        "FROM " +
        $"{TableNames.scheduledGames} " +
        "WHERE " +
        $"{PropertyNames.season}=" +
        $"{ParameterNames.season} " +
        "AND " +
        $"{PropertyNames.week}=" +
        $"{ParameterNames.week}" +
        "AND " +
        $"{PropertyNames.byeTeamId}=" +
        $"{ParameterNames.byeTeamId};";

    internal const string scheduledGames_home_byWeek =
        "SELECT " +
        $"{PropertyNames.id}, " +
        $"{PropertyNames.season}, " +
        $"{PropertyNames.week}, " +
        $"{PropertyNames.homeTeamId}, " +
        $"{PropertyNames.awayTeamId}, " +
        $"{PropertyNames.byeTeamId}, " +
        $"{PropertyNames.dayOfWeekIdx} " +
        "FROM " +
        $"{TableNames.scheduledGames} " +
        "WHERE " +
        $"{PropertyNames.season}=" +
        $"{ParameterNames.season} " +
        "AND " +
        $"{PropertyNames.week}=" +
        $"{ParameterNames.week}" +
        "AND " +
        $"{PropertyNames.homeTeamId}=" +
        $"{ParameterNames.homeTeamId};";

    internal const string scheduledGames_away_byWeek =
    "SELECT " +
    $"{PropertyNames.id}, " +
    $"{PropertyNames.season}, " +
    $"{PropertyNames.week}, " +
    $"{PropertyNames.homeTeamId}, " +
    $"{PropertyNames.awayTeamId}, " +
    $"{PropertyNames.byeTeamId}, " +
    $"{PropertyNames.dayOfWeekIdx} " +
    "FROM " +
    $"{TableNames.scheduledGames} " +
    "WHERE " +
    $"{PropertyNames.season}=" +
    $"{ParameterNames.season} " +
    "AND " +
    $"{PropertyNames.week}=" +
    $"{ParameterNames.week}" +
    "AND " +
    $"{PropertyNames.awayTeamId}=" +
    $"{ParameterNames.homeTeamId};";

    internal const string scheduledGames_week_byTeam =
        "SELECT " +
        $"{PropertyNames.id}, " +
        $"{PropertyNames.season}, " +
        $"{PropertyNames.week}, " +
        $"{PropertyNames.homeTeamId}, " +
        $"{PropertyNames.awayTeamId}, " +
        $"{PropertyNames.byeTeamId}, " +
        $"{PropertyNames.dayOfWeekIdx} " +
        "FROM " +
        $"{TableNames.scheduledGames} " +
        "WHERE " +
        $"{PropertyNames.awayTeamId}=" +
        $"{ParameterNames.awayTeamId} " +
        "|| " +
        $"{PropertyNames.homeTeamId}=" +
        $"{ParameterNames.homeTeamId} " +
        $"|| " +
        $"{PropertyNames.byeTeamId}=" +
        $"{ParameterNames.byeTeamId}" +
        "AND " +
        $"{PropertyNames.season}=" +
        $"{ParameterNames.season}" +
        "AND " +
        $"{PropertyNames.week}=" +
        $"{ParameterNames.week};";
}
