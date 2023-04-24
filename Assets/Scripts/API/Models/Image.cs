using System;

namespace GAAUBAGE_Game.API.Models
{
    public class Image : BaseModel
    {
        public Guid UserID { get; set; }
        public Guid ImageAnnotationID { get; set; }
        public string URI { get; set; } = string.Empty;
    }
}