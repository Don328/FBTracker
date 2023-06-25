using FBTracker.Server.Data.Records;
using FBTracker.Server.Data.Schema.Tables;
using FBTracker.Shared.GloblaConstants;
using MySqlConnector;

namespace FBTracker.Server.Data.Repo;

internal class SeasonPrepRepo{
    private readonly MySqlConnection _db;
    private int _season;

    internal SeasonPrepRepo(DataContext context)
    {
        _db = context.GetConnectionAsync()
            .GetAwaiter().GetResult();
    }

    internal SeasonPrepRepo WithSeason(int season)
    {
        _season = season;
        return this;
    }

    internal async Task<SeasonPrepRecord> Get()
    {
        if(_season >= StateConstants.seasonMin &&
            _season <= StateConstants.seasonMax) 
        {
            var record = await new SeasonPrepTable(_db)
                .WithSeason(_season)
                .ReadRecord();

            return await Task.FromResult(record);
        }

        return default!;
    }

    internal async Task<IEnumerable<SeasonPrepRecord>> GetAll()
    {
        var records = await new SeasonPrepTable(_db).ReadAll();
        return await Task.FromResult(records);
    }

    internal async Task Create()
    {
        var record = new SeasonPrepRecord(0, _season, false, false);
        await new SeasonPrepTable(_db).Create(record);
        await Task.CompletedTask;
    }

    internal async Task ConfirmTeams()
    {
        await new SeasonPrepTable(_db)
            .WithSeason(_season)
            .ConfirmSeasonTeams();
    }

    internal async Task ConfirmSchedule()
    {
        await new SeasonPrepTable(_db)
            .WithSeason(_season)
            .ConfirmSeasonSchedule();
    }
}
