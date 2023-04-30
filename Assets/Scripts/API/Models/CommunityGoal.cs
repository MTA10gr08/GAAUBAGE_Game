namespace GAAUBAGE_Game.API.Models
{
    public class CommunityGoal
    {
        public string TaskType { get; set; } = string.Empty;
        public uint TotalToDo;
        public uint DoneAll;
        public uint DoneYou;
    }
}