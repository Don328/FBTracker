namespace FBTracker.Server.Data.Records;

internal record GameRecord(
    int Id,
    int Season,
    int Week,
    int HomeTeam,
    int AwayTeam);
