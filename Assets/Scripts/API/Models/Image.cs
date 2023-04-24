using System;

namespace GAAUBAGE_Game.API.Models
{
    public class Image : BaseModel
    {
        public Guid User { get; set; }
        public Guid ImageAnnotation { get; set; }
        public string URI { get; set; } = string.Empty;
    }
}