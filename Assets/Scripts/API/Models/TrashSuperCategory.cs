using System;
using System.Collections.Generic;

namespace GAAUBAGE_Game.API.Models
{
    public class TrashSuperCategory : BaseModel
    {
        public List<Guid> Users { get; set; }
        public Guid SubImageAnnotation { get; set; }
        public string TrashSuperCategoryLabel { get; set; } = string.Empty;
    }
}