namespace FBTracker.Server.Data.Records;

public record TeamRecord(
    int Id,
    int Season,
    string Locale,
    string Name,
    string Abrev,
    int ConferenceIndex,
    int RegionIndex);
