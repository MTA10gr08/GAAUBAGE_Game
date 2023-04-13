using System;
namespace GAAUBAGE_Game.API.Models
{
    public class Segmentation : BaseModel
    {
        public Guid UserId { get; set; }
        public Guid TrashBoundingBoxId { get; set; }
        public MultiPolygon SegmentationPolygon { get; set; } = null!;
    }
}