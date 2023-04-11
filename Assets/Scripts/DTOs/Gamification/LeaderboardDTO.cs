using System.Collections.Generic;
namespace API.DTOs.Gamification
{
    public class LeaderboardDTO
    {
        ICollection<string> Aliases { get; set; } = new List<string>();
    }
}