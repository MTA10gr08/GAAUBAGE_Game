using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GAAUBAGE_Game.API.Models
{
    public class ImageAnnotation : BaseModel
    {
        public Guid id { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public string imageID { get; set; }
        public string[] backgroundClassifications { get; set; }
        public string[] contextClassifications { get; set; }
        public string[] subImages { get; set; }
        public string[] subImagesConsensus { get; set; }
    }
}
