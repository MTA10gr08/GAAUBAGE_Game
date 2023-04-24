using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GAAUBAGE_Game.API.Models
{
    public class ImageAnnotation : BaseModel
    {
        public Guid Image { get; set; }

        public List<Guid> BackgroundClassifications { get; set; }
        public Guid? BackgroundClassificationConsensus;

        public List<Guid> ContextClassifications { get; set; }  
        public Guid? ContextClassificationConsensus;

        public List<Guid> SubImages { get; set; }
        public Guid? SubImagesConsensus { get; set; }
        public bool IsInProgress;
        public bool IsComplete;
    }
}
