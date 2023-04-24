using GAAUBAGE_Game.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.API.Models
{
    public class SubImageAnnotation : BaseModel
    {
        public List<Guid> Users { get; set; }
        public Guid ImageAnnotation { get; set; }
        public List<BoundingBox> SubImages { get; set; }
    }
}
