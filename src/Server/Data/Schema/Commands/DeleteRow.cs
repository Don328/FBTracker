namespace FBTracker.Server.Data.Schema.Commands;

internal static class DeleteRow
{
    internal const string Team =
    @"DELETE FROM Teams
        WHERE Id=@id";
}
