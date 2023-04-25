using System;
using System.Collections.Generic;

namespace GAAUBAGE_Game.API.Models
{
    public class SubImageAnnotationGroup : BaseModel
    {
        public List<Guid> Users { get; set; }
        public Guid ImageAnnotation { get; set; }
        public List<BoundingBox> SubImageAnnotations { get; set; } = null!;
    }
}