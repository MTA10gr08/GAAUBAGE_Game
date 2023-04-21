using System;
using System.Collections.Generic;

namespace GAAUBAGE_Game.API.Models
{
    public class BackgroundClassification : BaseModel
    {
        public ICollection<Guid> Users { get; set; }
        public Guid ImageAnnotation { get; set; }
        public ICollection<string> BackgroundClassificationLabels { get; set; }
    }
}