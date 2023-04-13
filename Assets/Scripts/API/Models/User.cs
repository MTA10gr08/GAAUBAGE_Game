using System;
using System.Collections.Generic;
namespace GAAUBAGE_Game.API.Models
{
    public class User : BaseModel
    {
        public string Alias { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public ICollection<Guid> UserContextCategoryIds { get; set; } = new List<Guid>();
        public ICollection<Guid> UserBackgroundContextIds { get; set; } = new List<Guid>();
        public ICollection<Guid> UserTrashCountIds { get; set; } = new List<Guid>();
        public ICollection<Guid> UserTrashBoundingBoxIds { get; set; } = new List<Guid>();
        public ICollection<Guid> UserTrashSuperCategoryIds { get; set; } = new List<Guid>();
        public ICollection<Guid> UserTrashCategoryIds { get; set; } = new List<Guid>();
        public ICollection<Guid> UserSegmentationIds { get; set; } = new List<Guid>();
    }
}