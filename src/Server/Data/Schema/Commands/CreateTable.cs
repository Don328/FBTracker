using FBTracker.Server.Data.Schema.Constants;

namespace FBTracker.Server.Data.Schema.Commands;

internal static class CreateTable
{
    internal const string userState =
        "CREATE TABLE IF NOT EXISTS " +
        $"{TableNames.userState}(" +
        $"{PropertyNames.id} INTEGER PRIMARY KEY, " +
        $"{PropertyNames.season} INTEGER NOT NULL);";

    internal const string teams =
        "CREATE TABLE IF NOT EXISTS " +
        $"{TableNames.teams}(" +
        $"{PropertyNames.id} INTEGER PRIMARY KEY, " +
        $"{PropertyNames.season} INTEGER NOT NULL, " +
        $"{PropertyNames.locale} VARCHAR(25) NOT NULL, " +
        $"{PropertyNames.name} VARCHAR(25) NOT NULL, " +
        $"{PropertyNames.abrev} VARCHAR(3) NOT NULL, " +
        $"{PropertyNames.conference} INTEGER NOT NULL, " +
        $"{PropertyNames.region} INTEGER NOT NULL);";

    internal const string seasonPrep =
        "CREATE TABLE IF NOT EXISTS " +
        $"{TableNames.seasonPrep}(" +
        $"{PropertyNames.id} INTEGER PRIMARY KEY, " +
        $"{PropertyNames.season} INTEGER NOT NULL, " +
        $"{PropertyNames.teamsConfirmed} INTEGER NOT NULL, " +
        $"{PropertyNames.schedulesLoaded} INTEGER NOT NULL);";

    internal const string scheduledGames =
        "CREATE TABLE IF NOT EXISTS " +
        $"{TableNames.scheduledGames}(" +
        $"{PropertyNames.id} INTEGER PRIMARY KEY, " +
        $"{PropertyNames.season} INTEGER NOT NULL, " +
        $"{PropertyNames.week} INTEGER NOT NULL, " +
        $"{PropertyNames.homeTeamId} INTEGER NOT NULL, " +
        $"{PropertyNames.awayTeamId} INTEGER NOT NULL, " +
        $"{PropertyNames.byeTeamId} INTEGER NOT NULL, " +
        $"{PropertyNames.dayOfWeekIdx} INTEGER NOT NULL);";
}
