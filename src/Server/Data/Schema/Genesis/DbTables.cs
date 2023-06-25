using FBTracker.Server.Data.Schema.Commands;
using FBTracker.Server.Data.Schema.Tables;
using MySqlConnector;

namespace FBTracker.Server.Data.Schema.Genesis;

internal static class DbTables
{
    internal static async void EnsureExists(MySqlConnection conn)
    {

        conn.Open();
        await new MySqlCommand(CreateTable.userState, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(CreateTable.teams, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(CreateTable.seasonPrep, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(CreateTable.scheduledGames, conn).ExecuteNonQueryAsync();
        await conn.CloseAsync();
        await SeedUser_1(conn);
        await Seed2021Data(conn);
    }

    internal static async Task SeedUser_1(MySqlConnection conn)
    {
        var season = await new UserStateTable(conn)
            .WithUserId(1)
            .GetSelectedSeason();

        if (season < 1)
        {
            conn.Open();
            await new MySqlCommand(SeedData.user_1, conn).ExecuteNonQueryAsync();
            await conn.CloseAsync();
        }

        await Task.CompletedTask;
    }

    internal static async Task Seed2021Data(MySqlConnection conn)
    {
        var seasonPrep = await new SeasonPrepTable(conn)
            .WithSeason(2021)
            .ReadRecord();
        
        if (seasonPrep.Season == -1) await SeedSeasonPrep(conn);
        
        var teams = await new TeamsTable(conn)
            .WithSeason(2021)
            .ReadSeason();

        if (teams.Count() < 32) await SeedTeams(conn);
    }

    private static async Task SeedSeasonPrep(MySqlConnection conn)
    {
        conn.Open();
        await new MySqlCommand(SeedData.seasonPrep_2021, conn).ExecuteNonQueryAsync();
        await conn.CloseAsync();
    }

    private static async Task SeedTeams(MySqlConnection conn)
    {
        conn.Open();
        await new MySqlCommand(SeedData.team_1, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_2, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_3, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_4, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_5, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_6, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_7, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_8, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_9, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_10, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_11, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_12, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_13, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_14, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_15, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_16, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_17, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_18, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_19, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_20, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_21, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_22, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_23, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_24, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_25, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_26, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_27, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_28, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_29, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_30, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_31, conn).ExecuteNonQueryAsync();
        await new MySqlCommand(SeedData.team_32, conn).ExecuteNonQueryAsync();
        await conn.CloseAsync();
    }
}
