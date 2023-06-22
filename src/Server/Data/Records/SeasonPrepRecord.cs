namespace FBTracker.Server.Data.Records;

internal record SeasonPrepRecord(
    int Id,
    int Season,
    bool TeamsConfirmed,
    bool ScheduleConfirmed);
