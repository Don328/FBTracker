using FBTracker.Server.Data.Schema.Constants;

namespace FBTracker.Server.Data.Schema.Commands;

internal static class GetIdsInUse
{
    internal const string userState =
        $"SELECT  {PropertyNames.id} " +
        $"FROM {TableNames.userState} " +
        $"ORDER BY {PropertyNames.id};";

    internal const string teams =
        $"SELECT  {PropertyNames.id} " +
        $"FROM {TableNames.teams} " +
        $"ORDER BY {PropertyNames.id};";

    internal const string seasonPrep =
        $"SELECT {PropertyNames.id} " +
        $"FROM {TableNames.seasonPrep} " +
        $"ORDER BY {PropertyNames.id};";

    internal const string scheduledGame =
        $"SELECT {PropertyNames.id} " +
        $"FROM {TableNames.scheduledGames} " +
        $"ORDER BY {PropertyNames.id};";
}
