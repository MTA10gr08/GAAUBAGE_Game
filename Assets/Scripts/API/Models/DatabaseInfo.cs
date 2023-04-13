namespace GAAUBAGE_Game.API.Models
{
    public class DatabaseInfo
    {
        public uint TotalImages { get; set; }
        public uint TotalBackgroundClassified { get; set; }
        public uint TotalContextClassified { get; set; }
        public uint TotalTrashBoundingBoxed { get; set; }
        public uint TotalTrashSuperCategorised { get; set; }
        public uint TotalTrashCategorised { get; set; }
        public uint TotalSegmentated { get; set; }
    }
}