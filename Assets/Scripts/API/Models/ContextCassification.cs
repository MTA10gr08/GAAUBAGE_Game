using System;
using System.Collections.Generic;

namespace GAAUBAGE_Game.API.Models
{
    public class ContextClassification : BaseModel
    {
        public List<Guid> Users { get; set; }
        public Guid ImageAnnotation { get; set; }
        public string ContextClassificationLabel { get; set; } = string.Empty;
    }
}