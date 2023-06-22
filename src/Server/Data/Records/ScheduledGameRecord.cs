namespace FBTracker.Server.Data.Records;

internal record ScheduledGameRecord(
    int Id,
    int Season,
    int Week,
    int HomeTeamId,
    int AwayTeamId,
    int ByeTeamId,
    int DayOfWeekIdx);
