using System.Collections.Generic;
namespace GAAUBAGE_Game.API.Models
{
    public class MultiPolygon
    {
        public List<Polygon> Polygons { get; set; } = null!;
    }
}