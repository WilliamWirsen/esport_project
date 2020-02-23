using System.ComponentModel;

namespace esport.Models.Enums
{
    public enum GameType
    {
        [Description("overwatch")]
        Overwatch,
        [Description("cs-go")]
        CounterStrike,
        [Description("dota-2")]
        Dota2,
        [Description("league-of-legends")]
        LeagueOfLegends,
        [Description("rl")]
        RocketLeague,
        [Description("cod-mw")]
        CallofDutyModernWarfare
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
            GameType.RocketLeague
        };
    }
}
