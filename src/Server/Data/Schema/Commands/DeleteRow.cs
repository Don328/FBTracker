using FBTracker.Server.Data.Schema.Constants;

namespace FBTracker.Server.Data.Schema.Commands;

internal static class DeleteRow
{
    internal const string userState =
        "DELETE FROM " +
        $"{TableNames.userState} " +
        "WHERE " +
        $"{PropertyNames.id}=" +
        $"{ParameterNames.id};";

    internal const string team =
        "DELETE FROM " +
        $"{TableNames.teams} " +
        "WHERE " +
        $"{PropertyNames.id}=" +
        $"{ParameterNames.id};";

    internal const string seasonPrep =
        "DELETE FROM " +
        $"{TableNames.seasonPrep} " +
        "WHERE " +
        $"{PropertyNames.season}=" +
        $"{ParameterNames.season};";

    internal const string scheduledGame =
        "DELETE FROM " +
        $"{TableNames.scheduledGames} " +
        "WHERE " +
        $"{PropertyNames.season}=" +
        $"{ParameterNames.season} " +
        "AND " +
        $"{PropertyNames.week}=" +
        $"{ParameterNames.week};";
}
