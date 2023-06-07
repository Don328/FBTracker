namespace FBTracker.Server.Data.Schema.Commands;

internal static class ReadRow
{
    internal const string team_by_id =
    @"SELECT
            Id,
            Season,
            Locale,
            Name,
            Abrev,
            Conference,
            Region
        FROM Teams
        WHERE Id=@id";
}
