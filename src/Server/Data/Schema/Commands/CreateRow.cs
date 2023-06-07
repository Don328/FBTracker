namespace FBTracker.Server.Data.Schema.Commands;

internal static class CreateRow
{
    internal const string team =
    @"INSERT INTO Teams(
            Season, Locale, Name, 
            Abrev, Conference, Region)
        VALUES(
            @season @locale, @name, 
            @abrev, @conference, @region);
        SELECT last_insert_rowid();";
}
