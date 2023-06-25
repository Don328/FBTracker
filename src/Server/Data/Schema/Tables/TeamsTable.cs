using FBTracker.Server.Data.Mappers;
using FBTracker.Server.Data.Records;
using FBTracker.Server.Data.Schema.Commands;
using FBTracker.Server.Data.Schema.Constants;
using FBTracker.Shared.Models;
using MySqlConnector;
using System.IO;

namespace FBTracker.Server.Data.Schema.Tables;

internal class TeamsTable
{
    private readonly MySqlConnection _conn;
    private int _teamId;
    private int _season;

    public TeamsTable(MySqlConnection conn)
    {
        _conn = conn;
    }

    internal async Task<int> Create(Team team)
    {
        int id = await TableIdService.GetNextId(
            _conn, GetIdsInUse.teams);

        using var cmd = _conn.CreateCommand();
        cmd.CommandText = CreateRow.team;
        ParamBuilder.Build(cmd, ParameterNames.id, id);
        ParamBuilder.Build(cmd, ParameterNames.season, team.Season);
        ParamBuilder.Build(cmd, ParameterNames.locale, team.Locale);
        ParamBuilder.Build(cmd, ParameterNames.name, team.Name);
        ParamBuilder.Build(cmd, ParameterNames.abrev, team.Abrev);
        ParamBuilder.Build(cmd, ParameterNames.conference, (int)team.Conference);
        ParamBuilder.Build(cmd, ParameterNames.region, (int)team.Region);

        _conn.Open();
        await cmd.ExecuteNonQueryAsync();
        await _conn.CloseAsync();

        return await Task.FromResult(id);
    }

    internal TeamsTable WithId(int id)
    {
        _teamId = id;
        return this;
    }

    internal TeamsTable WithSeason(int season)
    {
        _season = season;
        return this;
    }

    internal async Task<Team> ReadFromId()
    {
        TeamRecord record = default!;
        using var cmd = _conn.CreateCommand();

        cmd.CommandText = ReadRow.team_by_id;
        ParamBuilder.Build(cmd, ParameterNames.id, _teamId);
        _conn.Open();
        using var reader = await cmd.ExecuteReaderAsync();
        while (reader.Read())
        {
            record = await ReadRecord(reader);
        }

        var team = TeamsMapper.ToEntity(record);
        await _conn.CloseAsync();
        return await Task.FromResult(team);
    }

    internal async Task<IEnumerable<TeamRecord>> ReadSeason()
    {
        var teams = new List<TeamRecord>();

        using var cmd = _conn.CreateCommand();
        cmd.CommandText = ReadTable.teamsBySeason;
        ParamBuilder.Build(cmd, ParameterNames.season, _season);

        _conn.Open();
        using var reader = await cmd.ExecuteReaderAsync();
        while (reader.Read())
        {
            teams.Add(await ReadRecord(reader));
        }

        await _conn.CloseAsync();
        return await Task.FromResult(teams);
    }

    internal async Task UpdateTeam(
        Team team)
    {
        using var cmd = _conn.CreateCommand();
        cmd.CommandText = UpdateRow.team;
        ParamBuilder.Build(cmd, ParameterNames.id, team.Id);
        ParamBuilder.Build(cmd, ParameterNames.locale, team.Locale);
        ParamBuilder.Build(cmd, ParameterNames.name, team.Name);
        ParamBuilder.Build(cmd, ParameterNames.abrev, team.Abrev);
        ParamBuilder.Build(cmd, ParameterNames.conference, (int)team.Conference);
        ParamBuilder.Build(cmd, ParameterNames.region, (int)team.Region);

        _conn.Open();
        await cmd.ExecuteNonQueryAsync();
        await _conn.CloseAsync();

        await Task.CompletedTask;
    }

    internal async Task Delete()
    {
        using var cmd = _conn.CreateCommand();
        cmd.CommandText = DeleteRow.team;
        ParamBuilder.Build(cmd, ParameterNames.id, _teamId);

        _conn.Open();
        await cmd.ExecuteNonQueryAsync();
        await _conn.CloseAsync();
    }

    private static async Task<TeamRecord> ReadRecord(
        MySqlDataReader reader)
    {
        var record = new TeamRecord(
            Id: reader.GetInt32(0),
            Season: reader.GetInt32(1),
            Locale: reader.GetString(2),
            Name: reader.GetString(3),
            Abrev: reader.GetString(4),
            ConferenceIndex: reader.GetInt32(5),
            RegionIndex: reader.GetInt32(6));

        return await Task.FromResult(record);
    }
}
