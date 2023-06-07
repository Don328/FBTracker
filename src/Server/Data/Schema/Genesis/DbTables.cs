using FBTracker.Server.Data.Schema.Commands;
using FBTracker.Server.Data.Schema.Tables;
using MySqlConnector;

namespace FBTracker.Server.Data.Schema.Genesis;

internal static class DbTables
{
    internal static void EnsureExists(MySqlConnection conn)
    {
        new MySqlCommand(CreateTable.Teams, conn).ExecuteNonQuery();
    }

    internal static async Task Seed2021Teams(MySqlConnection conn)
    {
        var teams = await TeamsTable.ReadSeason(conn, 2021);
        if (teams.Count() == 32) return;

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
    }
}
