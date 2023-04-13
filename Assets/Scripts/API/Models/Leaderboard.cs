using System.Collections.Generic;
namespace GAAUBAGE_Game.API.Models
{
    public class Leaderboard
    {
        ICollection<string> Aliases { get; set; } = new List<string>();
    }
}