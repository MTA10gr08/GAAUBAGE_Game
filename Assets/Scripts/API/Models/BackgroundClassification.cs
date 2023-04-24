using System;
using System.Collections.Generic;

namespace GAAUBAGE_Game.API.Models
{
    public class BackgroundClassification : BaseModel
    {
        public List<Guid> Users { get; set; }
        public Guid ImageAnnotation { get; set; }
        public List<string> BackgroundClassificationLabels { get; set; }
    }
}