namespace GAAUBAGE_Game.API.Models
{
    public class Coordinate
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double X => Longitude;
        public double Y => Latitude;
    }
}