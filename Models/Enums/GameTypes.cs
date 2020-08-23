using System.ComponentModel;

namespace esport.Models.Enums
{
    public enum GameType
    {
        [Description("overwatch")]
        Overwatch = 14,
        [Description("cs-go")]
        CounterStrike = 3,
        [Description("dota-2")]
        Dota2 = 4,
        [Description("league-of-legends")]
        LeagueOfLegends = 1,
        [Description("rl")]
        RocketLeague = 22,
        [Description("cod-mw")]
        CallofDutyModernWarfare,
        [Description("rb6")]
        RainbowSixSiege
    }
    public static class TypeGroups
    {
        public static GameType[] AllowedGameTypes =
        {
            GameType.Overwatch,
            GameType.CounterStrike,
            GameType.Dota2,
            GameType.LeagueOfLegends,
            GameType.CallofDutyModernWarfare,
            GameType.RocketLeague,
            GameType.RainbowSixSiege

        };
    }
}
