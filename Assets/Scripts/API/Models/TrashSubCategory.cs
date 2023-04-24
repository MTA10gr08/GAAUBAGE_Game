using System;
using System.Collections.Generic;

namespace GAAUBAGE_Game.API.Models
{
    public class TrashSubCategory : BaseModel
    {
        public List<Guid> Users { get; set; }
        public Guid ImageAnnotation { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}