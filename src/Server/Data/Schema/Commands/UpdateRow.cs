namespace FBTracker.Server.Data.Schema.Commands;

internal static class UpdateRow
{
    internal const string Team =
    @"UPDATE Teams
        SET Locale=@locale,
            Name=@name,
            Abrev=@abrev
            Converence=@conference,
            Region=@region
        WHERE Id=@id";
}
