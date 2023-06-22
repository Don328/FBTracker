using FBTracker.Server.Data.Records;
using FBTracker.Shared.Enums;
using FBTracker.Shared.Models;

namespace FBTracker.Server.Data.Mappers;

internal static class ScheduledGamesMapper
{
    internal static ScheduledGame ToEntity(ScheduledGameRecord record)
    {
        return new ScheduledGame()
        {
            Id = record.Id,
            Season = record.Season,
            Week = record.Week,
            HomeTeamId = record.HomeTeamId,
            AwayTeamId = record.AwayTeamId,
            ByeTeamId = record.ByeTeamId,
            GameDay = (GameDay)record.DayOfWeekIdx
        };
    }

    internal static IEnumerable<ScheduledGame> ToEntity(IEnumerable<ScheduledGameRecord> records)
    {
        var entities = new List<ScheduledGame>();
        foreach (var record in records)
        {
            entities.Add(ToEntity(record));
        }

        return entities;
    }

    internal static ScheduledGameRecord ToRecord(ScheduledGame entity)
    {
        return new ScheduledGameRecord(
            Id: entity.Id,
            Season: entity.Season,
            Week: entity.Week,
            HomeTeamId: entity.HomeTeamId,
            AwayTeamId: entity.AwayTeamId,
            ByeTeamId: entity.ByeTeamId,
            DayOfWeekIdx: (int)entity.GameDay);
    }

    internal static IEnumerable<ScheduledGameRecord> ToRecord(IEnumerable<ScheduledGame> entities)
    {
        var records = new List<ScheduledGameRecord>();
        foreach (var entity in entities)
        {
            records.Add(ToRecord(entity));
        }

        return records;
    }
}
