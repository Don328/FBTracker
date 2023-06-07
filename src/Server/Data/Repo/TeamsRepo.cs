using FBTracker.Server.Data.Mappers;
using FBTracker.Server.Data.Records;
using FBTracker.Server.Data.Schema.Tables;
using FBTracker.Shared.HardValues;
using FBTracker.Shared.Models;
using MySqlConnector;
using System.Security.Cryptography.Xml;

namespace FBTracker.Server.Data.Repo;

internal class TeamsRepo : IDisposable
{
    private readonly MySqlConnection _db;
    private int _season;
    private int _teamId;

    internal TeamsRepo(DataContext context)
    {
        _db = context.GetConnectionAsync()
            .GetAwaiter().GetResult();
    }

    public void Dispose()
    {
        _db.CloseAsync().GetAwaiter();
    }

    internal TeamsRepo FromSeason(int season)
    {
        _season = season;
        _teamId = default!;
        return this;
    }

    internal TeamsRepo FromTeam(int teamId)
    {
        // Not yet implemented in the .ToList() method

        _teamId = teamId;
        _season = default!;
        return this;
    }

    internal async Task<
        IEnumerable<Team>> ToList()
    {
        if (_season > StateConstants.seasonMin &&
            _season < StateConstants.seasonMax)
        {
            var teamRecords = await TeamsTable
                .ReadSeason(_db, _season);

            return TeamsMapper
                .ToEntity(teamRecords);
        }

        if (_teamId > 0 && _teamId < 33)
        {
            // Get team objects for multiple seasons
        }

        return await Task.FromResult(
            Enumerable.Empty<Team>());
    }
}
