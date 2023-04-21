using GAAUBAGE_Game.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Unity.Collections.NativeArray<T>;

namespace Assets.Scripts.API.Models
{
    public class SubImageAnnotation : BaseModel
    {
        public Guid UserID { get; set; }
        public Guid ImageAnnotationID { get; set; }
        public ICollection<BoundingBox> SubImages { get; set; }
    }
}
