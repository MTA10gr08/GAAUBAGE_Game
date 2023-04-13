using System;
using System.Collections;
using System.Collections.Generic;

namespace GAAUBAGE_Game.API.Models
{
    public class BackgroundClassification : BaseModel
    {
        public Guid UserId { get; }
        public Guid ImageId { get; set; }
        public ICollection<string> BackgroundCategory { get; set; }
    }
}