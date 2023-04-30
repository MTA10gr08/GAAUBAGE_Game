using static Unity.Collections.NativeArray<T>;

namespace GAAUBAGE_Game.API.Models
{
    public class DatabaseInfo
    {
        public uint TotalImages { get; set; }
        public uint TotalSkipped { get; set; }
        public uint TotalSubImages { get; set; }
        public uint TotalBackgroundClassified { get; set; }
        public uint TotalContextClassified { get; set; }
        public uint TotalSubImaged { get; set; }
        public uint TotalTrashSuperCategorised { get; set; }
        public uint TotalTrashSubCategorised { get; set; }
        public uint TotalSegmentated { get; set; }
    }
}