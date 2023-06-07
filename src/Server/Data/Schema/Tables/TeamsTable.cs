using FBTracker.Server.Data.Records;
using FBTracker.Server.Data.Schema.Commands;
using FBTracker.Shared.Models;
using MySqlConnector;
using System.IO;

namespace FBTracker.Server.Data.Schema.Tables;

internal class TeamsTable
{
    internal static async Task<int> Create(
        MySqlConnection conn, Team team)
    {
        int createdId = 0;
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = CreateRow.team;
            ParamBuilder.Build(cmd, "@season", team.Season);
            ParamBuilder.Build(cmd, "@locale", team.Locale);
            ParamBuilder.Build(cmd, "@name", team.Name);
            ParamBuilder.Build(cmd, "@abrev", team.Abrev);
            ParamBuilder.Build(cmd, "@conference", team.Conference);
            ParamBuilder.Build(cmd, "@region", team.Region);
            var response = await cmd.ExecuteScalarAsync();
            if (response != null) createdId = (int)response;
        }

        return await Task.FromResult(createdId);
    }

    internal static async Task<IEnumerable<TeamRecord>> ReadSeason(
        MySqlConnection conn, int season)
    {
        var teams = new List<TeamRecord>();
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = ReadTable.teamsBySeason;
            ParamBuilder.Build(cmd, "@season", season);

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
        }

        return await Task.FromResult(teams);
    }
}
