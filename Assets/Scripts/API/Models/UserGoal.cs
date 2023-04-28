namespace GAAUBAGE_Game.API.Models
{
    public class UserGoal
    {
        public string TaskType { get; set; } = string.Empty;
        public uint TotalToDo;
        public uint Done;
    }
}