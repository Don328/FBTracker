using FBTracker.Server.Data.Schema.Constants;

namespace FBTracker.Server.Data.Schema.Commands;

internal static class CreateRow
{
    internal const string userState =
        $"INSERT INTO {TableNames.userState}(" +
        $"{PropertyNames.id}, " +
        $"{PropertyNames.season}) " +
        "VALUES(" +
        $"{ParameterNames.id}, " +
        $"{ParameterNames.season});";

    internal const string team =
        $"INSERT INTO {TableNames.teams}(" +
        $"{PropertyNames.id}, " +
        $"{PropertyNames.season}, " +
        $"{PropertyNames.locale}, " +
        $"{PropertyNames.name}, " +
        $"{PropertyNames.abrev}, " +
        $"{PropertyNames.conference}, " +
        $"{PropertyNames.region}) " +
        "VALUES(" +
        $"{ParameterNames.id}, " +
        $"{ParameterNames.season}, " +
        $"{ParameterNames.locale}, " +
        $"{ParameterNames.name}, " +
        $"{ParameterNames.abrev}, " +
        $"{ParameterNames.conference}, " +
        $"{ParameterNames.region});";

    internal const string seasonPrep =
        $"INSERT INTO {TableNames.seasonPrep}(" +
        $"{PropertyNames.id}, " +
        $"{PropertyNames.season}, " +
        $"{PropertyNames.teamsConfirmed}, " +
        $"{PropertyNames.schedulesLoaded}) " +
        "VALUES(" +
        $"{ParameterNames.id}, " +
        $"{ParameterNames.season}, " +
        $"{ParameterNames.teamsConfirmed}, " +
        $"{ParameterNames.schedulesLoaded});";

    internal const string schedueldGame =
        $"INSERT INTO {TableNames.scheduledGames}(" +
        $"{PropertyNames.id}, " +
        $"{PropertyNames.season}, " +
        $"{PropertyNames.week}, " +
        $"{PropertyNames.homeTeamId}, " +
        $"{PropertyNames.awayTeamId}, " +
        $"{PropertyNames.byeTeamId}, " +
        $"{PropertyNames.dayOfWeekIdx}) " +
        "VALUES(" +
        $"{ParameterNames.id}, " +
        $"{ParameterNames.season}, " +
        $"{ParameterNames.week}, " +
        $"{ParameterNames.homeTeamId}, " +
        $"{ParameterNames.awayTeamId}, " +
        $"{ParameterNames.byeTeamId}, " +
        $"{ParameterNames.dayOfWeekIdx});";
}
