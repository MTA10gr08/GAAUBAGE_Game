using System.Collections.Generic;

namespace GAAUBAGE_Game.API.Models
{
    public class Leaderboard
    {
        public int CurrentUserSpot { get; set; }
        public List<Entry> Entries { get; set; }

        public class Entry
        {
            public string Alias { get; set; } = string.Empty;
            public int Score { get; set; }
        }
    }
}