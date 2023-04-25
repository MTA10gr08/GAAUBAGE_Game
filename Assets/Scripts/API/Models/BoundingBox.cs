using GAAUBAGE_Game.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAAUBAGE_Game.API.Models
{
    public class BoundingBox : BaseModel
    {
            public uint X { get; set; }
            public uint Y { get; set; }
            public uint Width { get; set; }
            public uint Height { get; set; }
    }
}
