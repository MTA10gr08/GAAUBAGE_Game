using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GAAUBAGE_Game.API.Models
{
    public class ImageAnnotation : BaseModel
    {
        public Guid ImageID { get; set; }

        public ICollection<Guid> BackgroundClassifications { get; set; }
        public Guid? BackgroundClassificationConsensus;

        public ICollection<Guid> ContextClassifications { get; set; }
        public Guid? ContextClassificationConsensus;

        public ICollection<Guid> SubImages { get; set; }
        public Guid? SubImagesConsensus { get; set; }
        public bool IsInProgress;
        public bool IsComplete;
    }
}
