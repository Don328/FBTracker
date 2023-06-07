namespace FBTracker.Server.Data.Schema.Commands;

internal static class ReadTable
{
    internal const string teamById =
    @"SELECT
            Id, Season, Locale, Name, 
            Abrev, Conference, Region
        FROM Teams
        WHERE Id=@id";

    internal const string teamsBySeason =
        @"SELECT
            Id, Season, Locale, Name,
            Abrev, Conference, Region
        FROM Teams
        Where Season=@season";
}
