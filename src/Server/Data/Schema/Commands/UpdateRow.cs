using FBTracker.Server.Data.Schema.Constants;

namespace FBTracker.Server.Data.Schema.Commands;

internal static class UpdateRow
{
    internal const string userState =
        "UPDATE " +
        $"{TableNames.userState} " +
        "SET " +
        $"{PropertyNames.season}={ParameterNames.season} " +
        "WHERE " +
        $"{PropertyNames.id}={ParameterNames.id};";

    internal const string team =
        "UPDATE " +
        $"{TableNames.teams} " +
        "SET " +
        $"{PropertyNames.locale}={ParameterNames.locale}, " +
        $"{PropertyNames.name}={ParameterNames.name}, " +
        $"{PropertyNames.abrev}={ParameterNames.abrev}, " +
        $"{PropertyNames.conference}={ParameterNames.conference}, " +
        $"{PropertyNames.region}={ParameterNames.region} " +
        "WHERE " +
        $"{PropertyNames.id}={ParameterNames.id};";

    internal const string seasonPrep =
        "UPDATE " +
        $"{TableNames.seasonPrep} " +
        "SET " +
        $"{PropertyNames.teamsConfirmed}={ParameterNames.teamsConfirmed}, " +
        $"{PropertyNames.schedulesLoaded}={ParameterNames.schedulesLoaded} " +
        "WHERE " +
        $"{PropertyNames.season}={ParameterNames.season};";
}
