using System.Collections.Generic;
namespace GAAUBAGE_Game.API.Models
{

    public class Polygon
    {
        public LinearRing Shell { get; set; } = null!;
        public List<LinearRing> Holes { get; set; } = null!;
    }
}