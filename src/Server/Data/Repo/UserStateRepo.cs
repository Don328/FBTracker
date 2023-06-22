using FBTracker.Server.Data.Records;
using FBTracker.Server.Data.Schema.Tables;
using MySqlConnector;

namespace FBTracker.Server.Data.Repo;

internal class UserStateRepo
{
    private readonly MySqlConnection _db;
    private  int _id;
    private UserStateRecord _record = default!;

    internal UserStateRepo(DataContext context)
    {
        _db = context.GetConnectionAsync()
            .GetAwaiter().GetResult();
    }

    internal UserStateRepo WithId(int id)
    {
        _id = id;
        return this;
    }

    internal UserStateRepo WithRecord(UserStateRecord record)
    {
        _record = record;
        return this;
    }

    internal async Task<int> GetSeason()
    {
        var season = await UserStateTable
            .GetSelectedSeason(_db, _id);

        return await Task.FromResult(season);
    }
    internal async Task UpdateRecord()
    {
        await UserStateTable.Update(_db, _record);
    }
}
