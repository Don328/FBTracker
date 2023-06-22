using FBTracker.Server.Data.Mappers;
using FBTracker.Server.Data.Records;
using FBTracker.Server.Data.Schema.Commands;
using FBTracker.Server.Data.Schema.Constants;
using FBTracker.Shared.Models;
using MySqlConnector;
using System.IO;

namespace FBTracker.Server.Data.Schema.Tables;

internal static class TeamsTable
{
    internal static async Task<int> Create(
        MySqlConnection conn, Team team)
    {
        int id = await TableIdService.GetNextId(
            conn, GetIdsInUse.teams);

        using var cmd = conn.CreateCommand();
        cmd.CommandText = CreateRow.team;
        ParamBuilder.Build(cmd, ParameterNames.id, id);
        ParamBuilder.Build(cmd, ParameterNames.season, team.Season);
        ParamBuilder.Build(cmd, ParameterNames.locale, team.Locale);
        ParamBuilder.Build(cmd, ParameterNames.name, team.Name);
        ParamBuilder.Build(cmd, ParameterNames.abrev, team.Abrev);
        ParamBuilder.Build(cmd, ParameterNames.conference, (int)team.Conference);
        ParamBuilder.Build(cmd, ParameterNames.region, (int)team.Region);

        conn.Open();
        await cmd.ExecuteNonQueryAsync();
        await conn.CloseAsync();

        return await Task.FromResult(id);
    }

    internal static async Task<Team> ReadFromId(
        MySqlConnection conn,
        int id)
    {
        TeamRecord record = default!;
        using var cmd = conn.CreateCommand();

        cmd.CommandText = ReadRow.team_by_id;
        ParamBuilder.Build(cmd, ParameterNames.id, id);
        conn.Open();
        using var reader = await cmd.ExecuteReaderAsync();
        while (reader.Read())
        {
            record = new(
                Id: reader.GetInt32(0),
                Season: reader.GetInt32(1),
                Locale: reader.GetString(2),
                Name: reader.GetString(3),
                Abrev: reader.GetString(4),
                ConferenceIndex: reader.GetInt32(5),
                RegionIndex: reader.GetInt32(6));
        }

        var team = TeamsMapper.ToEntity(record);
        await conn.CloseAsync();
        return await Task.FromResult(team);
    }

    internal static async Task<IEnumerable<TeamRecord>> ReadSeason(
        MySqlConnection conn, int season)
    {
        var teams = new List<TeamRecord>();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = ReadTable.teamsBySeason;
        ParamBuilder.Build(cmd, ParameterNames.season, season);

        conn.Open();
        using var reader = await cmd.ExecuteReaderAsync();
        while (reader.Read())
        {
            teams.Add(new TeamRecord(
                Id: reader.GetInt32(0),
                Season: reader.GetInt32(1),
                Locale: reader.GetString(2),
                Name: reader.GetString(3),
                Abrev: reader.GetString(4),
                ConferenceIndex: reader.GetInt32(5),
                RegionIndex: reader.GetInt32(6)));
        }

        await conn.CloseAsync();
        return await Task.FromResult(teams);
    }

    internal static async Task UpdateTeam(
        MySqlConnection conn,
        Team team)
    {
        using var cmd = conn.CreateCommand();
        cmd.CommandText = UpdateRow.team;
        ParamBuilder.Build(cmd, ParameterNames.id, team.Id);
        ParamBuilder.Build(cmd, ParameterNames.locale, team.Locale);
        ParamBuilder.Build(cmd, ParameterNames.name, team.Name);
        ParamBuilder.Build(cmd, ParameterNames.abrev, team.Abrev);
        ParamBuilder.Build(cmd, ParameterNames.conference, (int)team.Conference);
        ParamBuilder.Build(cmd, ParameterNames.region, (int)team.Region);

        conn.Open();
        await cmd.ExecuteNonQueryAsync();
        await conn.CloseAsync();

        await Task.CompletedTask;
    }

    internal static async Task Delete(
        MySqlConnection conn,
        int id)
    {
        using var cmd = conn.CreateCommand();
        cmd.CommandText = DeleteRow.team;
        ParamBuilder.Build(cmd, ParameterNames.id, id);

        conn.Open();
        await cmd.ExecuteNonQueryAsync();
        await conn.CloseAsync();
    }
}
