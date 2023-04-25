using System;
using System.Collections.Generic;

namespace GAAUBAGE_Game.API.Models
{
    public class TrashSubCategory : BaseModel
    {
        public List<Guid> Users { get; set; }
        public Guid SubImageAnnotation { get; set; }
        public string TrashSubCategoryLabel { get; set; } = string.Empty;
    }
}