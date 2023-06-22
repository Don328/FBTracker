using FBTracker.Server.Data.Records;
using FBTracker.Server.Data.Repo;
using FBTracker.Shared.Models;
using System.Reflection.Metadata.Ecma335;

namespace FBTracker.Server.Data.Services;

public class StateService
{
    private readonly DataContext _db;

    public StateService(
        DataContext db)
    {
        _db = db;
    }

    internal async Task<int> SelectedSeason(int userId)
    {
        var season = await new UserStateRepo(_db)
            .WithId(userId)
            .GetSeason();

        return await Task.FromResult(season);
    }

    internal async Task SelectSesason(UserStateRecord userState)
    {
        await new UserStateRepo(_db)
            .WithRecord(userState)
            .UpdateRecord();

        await Task.CompletedTask;
    }

    internal async Task<bool> TeamsConfirmed(int season)
    {
        var seasonPrep = await GetSeasonPrepRecord(season);
        return seasonPrep.TeamsConfirmed;
    }

    internal async Task<bool> ScheduleConfirmed(int season)
    {
        var seasonPrep = await GetSeasonPrepRecord(season);
        return seasonPrep.ScheduleConfirmed;
    }

    internal async Task ConfirmTeams(int season)
    {
        await new SeasonPrepRepo(_db)
            .WithSeason(season)
            .ConfirmTeams();
    }


    internal async Task ConfirmSchedule(int season)
    {
        await new SeasonPrepRepo(_db)
            .WithSeason(season)
            .ConfirmSchedule();
    }

    private async Task<SeasonPrepRecord> GetSeasonPrepRecord(int season)
    {
        var repo = new  SeasonPrepRepo(_db)
            .WithSeason(season);

        var seasonPrep = await repo.Get();
        if (seasonPrep.Season < 1)
        {
            await repo.Create();
            seasonPrep = await repo.Get();
        }

        return await Task.FromResult(seasonPrep);
    }
}
