using System;
using System.Collections.Generic;
namespace GAAUBAGE_Game.API.Models
{
    public class User : BaseModel
    {
        public string Alias { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public ICollection<Guid> ContextCategories { get; set; } = new List<Guid>();
        public ICollection<Guid> BackgroundContexts { get; set; } = new List<Guid>();
        public ICollection<Guid> SubImageGroups { get; set; } = new List<Guid>();
        public ICollection<Guid> TrashSuperCategories { get; set; } = new List<Guid>();
        public ICollection<Guid> TrashSubCategories { get; set; } = new List<Guid>();
        public ICollection<Guid> Segmentations { get; set; } = new List<Guid>();
    }
}