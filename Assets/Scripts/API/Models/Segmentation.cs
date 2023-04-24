using System;
using System.Collections.Generic;

namespace GAAUBAGE_Game.API.Models
{
    public class Segmentation : BaseModel
    {
        public List<Guid> Users { get; set; }
        public Guid ImageAnnotation { get; set; }
        public MultiPolygon SegmentationPolygon { get; set; } = null!;
    }
}