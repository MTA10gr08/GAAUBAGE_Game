using System;
using System.Collections.Generic;
using Assets.Scripts.API.Models;

namespace GAAUBAGE_Game.API.Models
{
    public class SubImageGroup : BaseModel
    {
        public List<Guid> Users { get; set; }
        public Guid ImageAnnotation { get; set; }
        public List<BoundingBox> SubImages { get; set; } = null!;
    }
}