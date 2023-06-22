using FBTracker.Server.Data.Schema.Constants;

namespace FBTracker.Server.Data.Schema.Genesis;

internal static class SeedData
{
    private const string userState_header =
        "INSERT INTO " +
        $"{TableNames.userState}(" +
        $"{PropertyNames.id}, " +
        $"{PropertyNames.season}) ";

    private const string teams_header =
        "INSERT INTO " +
        $"{TableNames.teams}(" +
        $"{PropertyNames.id}, " +
        $"{PropertyNames.season}, " +
        $"{PropertyNames.locale}, " +
        $"{PropertyNames.name}, " +
        $"{PropertyNames.abrev}, " +
        $"{PropertyNames.conference}, " +
        $"{PropertyNames.region}) ";

    private const string seasonPrep_header =
        "INSERT INTO " +
        $"{TableNames.seasonPrep}(" +
        $"{PropertyNames.id}, " +
        $"{PropertyNames.season}, " +
        $"{PropertyNames.teamsConfirmed}," +
        $"{PropertyNames.schedulesLoaded}) ";

    internal const string user_1 =
        userState_header + @"
        VALUES(
            1,
            2022);";

    internal const string team_1 =
        teams_header + @"
        VALUES(
            1,
            2021,
            'Arizona',
            'Cardinals',
            'AZ',
            1,
            3)";

    internal const string team_2 =
        teams_header + @"
        VALUES(
            2,
            2021,
            'Atlanta',
            'Falcons',
            'ATL',
            1,
            1)";

    internal const string team_3 =
        teams_header + @"
        VALUES(
            3,
            2021,
            'Baltimore',
            'Ravens',
            'BAL',
            0,
            0)";

    internal const string team_4 =
        teams_header + @"
        VALUES(
            4,
            2021,
            'Buffalo',
            'Bills',
            'BUF',
            0,
            2)";

    internal const string team_5 =
        teams_header + @"
        VALUES(
            5,
            2021,
            'Carolina',
            'Panthers',
            'CAR',
            1,
            1)";

    internal const string team_6 =
        teams_header + @"
        VALUES(
            6,
            2021,
            'Chicago',
            'Bears',
            'CHI',
            1,
            0)";

    internal const string team_7 =
        teams_header + @"
        VALUES(
            7,
            2021,
            'Cincinnati',
            'Bengals',
            'CIN',
            0,
            0)";

    internal const string team_8 =
        teams_header + @"
        VALUES(
            8,
            2021,
            'Cleavland',
            'Browns',
            'CLE',
            0,
            0)";

    internal const string team_9 =
        teams_header + @"
        VALUES(
            9,
            2021,
            'Dallas',
            'Cowboys',
            'DAL',
            1,
            2)";

    internal const string team_10 =
        teams_header + @"
        VALUES(
            10,
            2021,
            'Denver',
            'Broncos',
            'DEN',
            0,
            3)";

    internal const string team_11 =
        teams_header + @"
        VALUES(
            11,
            2021,
            'Detroit',
            'Lions',
            'DET',
            1,
            0)";

    internal const string team_12 =
        teams_header + @"
        VALUES(
            12,
            2021,
            'Green Bay',
            'Packers',
            'GB',
            1,
            0)";

    internal const string team_13 =
        teams_header + @"
        VALUES(
            13,
            2021,
            'Houston',
            'Texans',
            'HOU',
            0,
            1)";

    internal const string team_14 =
        teams_header + @"
        VALUES(
            14,
            2021,
            'Indianapolis',
            'Colts',
            'IND',
            0,
            1)";

    internal const string team_15 =
        teams_header + @"
        VALUES(
            15,
            2021,
            'Jacksonville',
            'Jaguars',
            'JAX',
            0,
            1)";

    internal const string team_16 =
        teams_header + @"
        VALUES(
            16,
            2021,
            'Kansas City',
            'Chiefs',
            'KC',
            0,
            3)";

    internal const string team_17 =
        teams_header + @"
        VALUES(
            17,
            2021,
            'Oakland',
            'Raiders',
            'OAK',
            0,
            3)";

    internal const string team_18 =
        teams_header + @"
        VALUES(
            18,
            2021,
            'San Diego',
            'Chargers',
            'SD',
            0,
            3)";

    internal const string team_19 =
        teams_header + @"
        VALUES(
            19,
            2021,
            'St Lois',
            'Rams',
            'STL',
            1,
            3)";

    internal const string team_20 =
        teams_header + @"
        VALUES(
            20,
            2021,
            'Miami',
            'Dolphins',
            'MIA',
            0,
            2)";

    internal const string team_21 =
        teams_header + @"
        VALUES(
            21,
            2021,
            'Minnasota',
            'Vikings',
            'MIN',
            1,
            0)";

    internal const string team_22 =
        teams_header + @"
        VALUES(
            22,
            2021,
            'New England',
            'Patriots',
            'NE',
            0,
            2)";

    internal const string team_23 =
        teams_header + @"
        VALUES(
            23,
            2021,
            'New Orleans',
            'Saints',
            'NO',
            1,
            1)";

    internal const string team_24 =
        teams_header + @"
        VALUES(
            24,
            2021,
            'New York',
            'Giants',
            'NYG',
            1,
            2)";

    internal const string team_25 =
        teams_header + @"
        VALUES(
            25,
            2021,
            'New York',
            'Jets',
            'NYJ',
            0,
            2)";

    internal const string team_26 =
        teams_header + @"
        VALUES(
            26,
            2021,
            'Philidelphia',
            'Eagles',
            'PHI',
            1,
            2)";

    internal const string team_27 =
        teams_header + @"
        VALUES(
            27,
            2021,
            'Pittsburgh',
            'Steelers',
            'PIT',
            0,
            0)";

    internal const string team_28 =
        teams_header + @"
        VALUES(
            28,
            2021,
            'San Francisco',
            '49ers',
            'SF',
            1,
            3)";

    internal const string team_29 =
        teams_header + @"
        VALUES(
            29,
            2021,
            'Seattle',
            'Seahawks',
            'SEA',
            1,
            3)";

    internal const string team_30 =
        teams_header + @"
        VALUES(
            30,
            2021,
            'Tampa Bay',
            'Buccanners',
            'TB',
            1,
            1)";

    internal const string team_31 =
        teams_header + @"
        VALUES(
            31,
            2021,
            'Tennessee',
            'Titans',
            'TEN',
            0,
            1)";

    internal const string team_32 =
        teams_header + @"
        VALUES(
            32,
            2021,
            'Washington',
            'Football Team',
            'WAS',
            1,
            2)";

    internal const string seasonPrep_2021 =
        seasonPrep_header + @"
        VALUES(
            1,
            2021,
            1,
            0)";
}
