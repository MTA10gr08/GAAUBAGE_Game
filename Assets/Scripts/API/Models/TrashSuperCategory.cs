using System;
using System.Collections.Generic;

namespace GAAUBAGE_Game.API.Models
{
    public class TrashSuperCategory : BaseModel
    {
        public List<Guid> Users { get; set; }
        public Guid ImageAnnotation { get; set; }
        public string SuperCategory { get; set; } = string.Empty;
    }
}