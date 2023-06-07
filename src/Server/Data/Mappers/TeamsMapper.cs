using FBTracker.Server.Data.Records;
using FBTracker.Shared.Enums;
using FBTracker.Shared.Models;
using System.IO;

namespace FBTracker.Server.Data.Mappers;

internal static class TeamsMapper
{
    internal static Team ToEntity(TeamRecord record)
    {
        return new Team()
        {
            Id = record.Id,
            Season = record.Season,
            Locale = record.Locale,
            Name = record.Name,
            Abrev = record.Abrev,
            Conference = (Conference)record.ConferenceIndex,
            Region = (Region)record.RegionIndex
        };
    }

    internal static IEnumerable<Team> ToEntity(IEnumerable<TeamRecord> records)
    {
        var entities = new List<Team>();
        foreach (var record in records)
        {
            entities.Add(ToEntity(record));
        }

        return entities;
    }

    internal static TeamRecord ToRecord(Team entity)
    {
        return new TeamRecord(
            Id: entity.Id,
            Season: entity.Season,
            Locale: entity.Locale,
            Name: entity.Name,
            Abrev: entity.Abrev,
            ConferenceIndex: (int)entity.Conference,
            RegionIndex: (int)entity.Region);
    }

    internal static List<TeamRecord> ToRecord(List<Team> entities)
    {
        var records = new List<TeamRecord>();
        foreach (var entity in entities)
        {
            records.Add(ToRecord(entity));
        }

        return records;
    }
}
