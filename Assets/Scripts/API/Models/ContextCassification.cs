using System;
using System.Collections.Generic;

namespace GAAUBAGE_Game.API.Models
{
    public class ContextClassification : BaseModel
    {
        public ICollection<Guid> Users { get; set; }
        public Guid ImageAnnotation { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}