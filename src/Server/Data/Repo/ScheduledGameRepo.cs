﻿using FBTracker.Server.Data.Mappers;
using FBTracker.Server.Data.Schema.Tables;
using FBTracker.Shared.Models;
using MySqlConnector;

namespace FBTracker.Server.Data.Repo;

internal class ScheduledGameRepo
{
    private readonly MySqlConnection _db;
    private int _season;
    private int _week;
    private int _teamId;

    public ScheduledGameRepo(DataContext context)
    {
        _db = context.GetConnectionAsync()
            .GetAwaiter().GetResult();
    }

    internal ScheduledGameRepo WithSeason(int season)
    {
        _season = season;
        return this;
    }

    internal ScheduledGameRepo WithWeek(int week)
    { 
        _week = week; 
        return this; 
    }

    internal ScheduledGameRepo WithTeamId(int teamId)
    {
        _teamId = teamId;
        return this;
    }

    internal async Task Create(ScheduledGame scheduledGame)
    {
        await new ScheduledGamesTable(_db)
            .Create(scheduledGame);
        await Task.CompletedTask;
    }

    internal async Task<IEnumerable<ScheduledGame>> GetTeamSeason()
    {
        return await new ScheduledGamesTable(_db)
            .WithSeason(_season)
            .WithTeamId(_teamId)
            .ReadTeamSeason();
    }

    internal async Task<ScheduledGame> GetTeamWeek()
    {
        return await new ScheduledGamesTable(_db)
            .WithTeamId (_teamId)
            .WithSeason (_season)
            .WithWeek(_week)
            .ReadTeamWeek();
    }

    internal async Task<IEnumerable<ScheduledGame>> GetSeason()
    {
        return await new ScheduledGamesTable(_db)
            .WithSeason(_season)
            .ReadSeason();
    }

    internal async Task<IEnumerable<ScheduledGame>> GetWeek()
    {
        return await new ScheduledGamesTable(_db)
            .WithSeason(_season)
            .WithWeek(_week)
            .ReadWeek();
    }
}
