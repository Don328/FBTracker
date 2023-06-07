namespace FBTracker.Server.Data.Schema.Genesis;

internal static class SeedData
{
    private const string header =
        @"INSERT INTO Teams(
            Id,
            Season,
            Locale,
            Name,
            Abrev,
            Conference,
            Region)";

    internal const string team_1 =
        header + @"
        VALUES(
            1,
            2021,
            'Arizona',
            'Cardinals',
            'AZ',
            1,
            3)";

    internal const string team_2 =
        header + @"
        VALUES(
            2,
            2021,
            'Atlanta',
            'Falcons',
            'ATL',
            1,
            1)";

    internal const string team_3 =
        header + @"
        VALUES(
            3,
            2021,
            'Baltimore',
            'Ravens',
            'BAL',
            0,
            0)";

    internal const string team_4 =
        header + @"
        VALUES(
            4,
            2021,
            'Buffalo',
            'Bills',
            'BUF',
            0,
            2)";

    internal const string team_5 =
        header + @"
        VALUES(
            5,
            2021,
            'Carolina',
            'Panthers',
            'CAR',
            1,
            1)";

    internal const string team_6 =
        header + @"
        VALUES(
            6,
            2021,
            'Chicago',
            'Bears',
            'CHI',
            1,
            0)";

    internal const string team_7 =
        header + @"
        VALUES(
            7,
            2021,
            'Cincinnati',
            'Bengals',
            'CIN',
            0,
            0)";

    internal const string team_8 =
        header + @"
        VALUES(
            8,
            2021,
            'Cleavland',
            'Browns',
            'CLE',
            0,
            0)";

    internal const string team_9 =
        header + @"
        VALUES(
            9,
            2021,
            'Dallas',
            'Cowboys',
            'DAL',
            1,
            2)";

    internal const string team_10 =
        header + @"
        VALUES(
            10,
            2021,
            'Denver',
            'Broncos',
            'DEN',
            0,
            3)";

    internal const string team_11 =
        header + @"
        VALUES(
            11,
            2021,
            'Detroit',
            'Lions',
            'DET',
            1,
            0)";

    internal const string team_12 =
        header + @"
        VALUES(
            12,
            2021,
            'Green Bay',
            'Packers',
            'GB',
            1,
            0)";

    internal const string team_13 =
        header + @"
        VALUES(
            13,
            2021,
            'Houston',
            'Texans',
            'HOU',
            0,
            1)";

    internal const string team_14 =
        header + @"
        VALUES(
            14,
            2021,
            'Indianapolis',
            'Colts',
            'IND',
            0,
            1)";

    internal const string team_15 =
        header + @"
        VALUES(
            15,
            2021,
            'Jacksonville',
            'Jaguars',
            'JAX',
            0,
            1)";

    internal const string team_16 =
        header + @"
        VALUES(
            16,
            2021,
            'Kansas City',
            'Chiefs',
            'KC',
            0,
            3)";

    internal const string team_17 =
        header + @"
        VALUES(
            17,
            2021,
            'Oakland',
            'Raiders',
            'OAK',
            0,
            3)";

    internal const string team_18 =
        header + @"
        VALUES(
            18,
            2021,
            'San Diego',
            'Chargers',
            'SD',
            0,
            3)";

    internal const string team_19 =
        header + @"
        VALUES(
            19,
            2021,
            'St Lois',
            'Rams',
            'STL',
            1,
            3)";

    internal const string team_20 =
        header + @"
        VALUES(
            20,
            2021,
            'Miami',
            'Dolphins',
            'MIA',
            0,
            2)";

    internal const string team_21 =
        header + @"
        VALUES(
            21,
            2021,
            'Minnasota',
            'Vikings',
            'MIN',
            1,
            0)";

    internal const string team_22 =
        header + @"
        VALUES(
            22,
            2021,
            'New England',
            'Patriots',
            'NE',
            0,
            2)";

    internal const string team_23 =
        header + @"
        VALUES(
            23,
            2021,
            'New Orleans',
            'Saints',
            'NO',
            1,
            1)";

    internal const string team_24 =
        header + @"
        VALUES(
            24,
            2021,
            'New York',
            'Giants',
            'NYG',
            1,
            2)";

    internal const string team_25 =
        header + @"
        VALUES(
            25,
            2021,
            'New York',
            'Jets',
            'NYJ',
            0,
            2)";

    internal const string team_26 =
        header + @"
        VALUES(
            26,
            2021,
            'Philidelphia',
            'Eagles',
            'PHI',
            1,
            2)";

    internal const string team_27 =
        header + @"
        VALUES(
            27,
            2021,
            'Pittsburgh',
            'Steelers',
            'PIT',
            0,
            0)";

    internal const string team_28 =
        header + @"
        VALUES(
            28,
            2021,
            'San Francisco',
            '49ers',
            'SF',
            1,
            3)";

    internal const string team_29 =
        header + @"
        VALUES(
            29,
            2021,
            'Seattle',
            'Seahawks',
            'SEA',
            1,
            3)";

    internal const string team_30 =
        header + @"
        VALUES(
            30,
            2021,
            'Tampa Bay',
            'Buccanners',
            'TB',
            1,
            1)";

    internal const string team_31 =
        header + @"
        VALUES(
            31,
            2021,
            'Tennessee',
            'Titans',
            'TEN',
            0,
            1)";

    internal const string team_32 =
        header + @"
        VALUES(
            32,
            2021,
            'Washington',
            'Football Team',
            'WAS',
            1,
            2)";
}
