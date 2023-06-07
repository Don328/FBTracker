namespace FBTracker.Server.Data.Schema.Commands;

internal static class CreateTable
{
    internal const string Teams =
    @"CREATE TABLE IF NOT EXISTS
            Teams(
                Id          INTEGER         PRIMARY KEY,
                Season      INTEGER         NOT NULL,
                Locale      VARCHAR(25)     NOT NULL,
                Name        VARCHAR(25)     NOT NULL,
                Abrev       VARCHAR(3)      NOT NULL,
                Conference  INTEGER         NOT NULL,
                Region      INTEGER         NOT NULL)";
}
