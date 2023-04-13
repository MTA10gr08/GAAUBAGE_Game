using System.Drawing;
using System;
using System.Collections.Generic;
namespace GAAUBAGE_Game.API.Models
{
    public class TrashBoundingBox : BaseModel
    {
        public Guid UserId { get; set; }
        public Guid TrashCountId { get; set; }
        public List<Rectangle> BoundingBoxs { get; set; } = null!;
    }
}