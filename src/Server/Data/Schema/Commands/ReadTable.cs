using FBTracker.Server.Data.Schema.Constants;

namespace FBTracker.Server.Data.Schema.Commands;

internal static class ReadTable
{
    internal const string userState =
        "SELECT " +
        $"{PropertyNames.id}, " +
        $"{PropertyNames.season} " +
        "FROM " +
        $"{TableNames.userState};";

    internal const string teamsBySeason =
        "SELECT " +
            $"{PropertyNames.id}, " +
            $"{PropertyNames.season}, " +
            $"{PropertyNames.locale}, " +
            $"{PropertyNames.name}, " +
            $"{PropertyNames.abrev}, " +
            $"{PropertyNames.conference}, " +
            $"{PropertyNames.region} " +
        "FROM " +
            $"{TableNames.teams} " +
        "WHERE " +
            $"{PropertyNames.season}=" +
            $"{ParameterNames.season};";

    internal const string seasonPrep =
        "SELECT " +
            $"{PropertyNames.id}, " +
            $"{PropertyNames.season}, " +
            $"{PropertyNames.teamsConfirmed}, " +
            $"{PropertyNames.schedulesLoaded} " +
        "FROM " +
            $"{TableNames.seasonPrep};";

    internal const string scheduledGames_bySeason =
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
            $"{ParameterNames.season};";

    internal const string scheduledGames_byTeam =
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
            $"{PropertyNames.homeTeamId}=" +
            $"{ParameterNames.homeTeamId} " +
        "|| " +
            $"{PropertyNames.awayTeamId}=" +
            $"{ParameterNames.awayTeamId} " +
        "|| " +
            $"{PropertyNames.byeTeamId}=" +
            $"{ParameterNames.byeTeamId} " +
        "AND " +
            $"{PropertyNames.season}=" +
            $"{ParameterNames.season};";

    internal const string scheduledGames_byWeek =
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
            $"{ParameterNames.week};";
}
