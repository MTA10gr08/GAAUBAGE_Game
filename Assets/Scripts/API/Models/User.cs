using System;
using System.Collections.Generic;
namespace GAAUBAGE_Game.API.Models
{
    public class User : BaseModel
    {
        public string Alias { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public uint Score { get; set; }
        public uint Level { get; set; }
        public List<Guid> Images { get; set; } = new List<Guid>();
        public List<Guid> Skipped { get; set; } = new List<Guid>();
        public List<Guid> BackgroundClassifications { get; set; } = new List<Guid>();
        public List<Guid> ContextClassifications { get; set; } = new List<Guid>();
        public List<Guid> SubImageGroups { get; set; } = new List<Guid>();
        public List<Guid> TrashSuperCategories { get; set; } = new List<Guid>();
        public List<Guid> TrashSubCategories { get; set; } = new List<Guid>();
        public List<Guid> Segmentations { get; set; } = new List<Guid>();
    }
}