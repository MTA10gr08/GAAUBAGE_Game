using System;
using System.Collections.Generic;

namespace GAAUBAGE_Game.API.Models
{
    public class SubImageAnnotation : BaseModel
    {
        public uint X { get; set; }
        public uint Y { get; set; }
        public uint Width { get; set; }
        public uint Height { get; set; }

        public Guid? SubImageAnnotationGroup { get; set; }

        public List<Guid> TrashSuperCategories { get; set; } = new List<Guid>();
        public Guid? TrashSuperCategoriesConsensus { get; set; }

        public List<Guid> TrashSubCategories { get; set; } = new List<Guid>();
        public Guid? TrashSubCategoriesConsensus { get; set; }

        public List<Guid> Segmentations { get; set; } = new List<Guid>();

        public bool IsInProgress { get; set; }
        public bool IsComplete { get; set; }
    }
}
