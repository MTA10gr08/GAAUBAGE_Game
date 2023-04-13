using System;
namespace GAAUBAGE_Game.API.Models
{
    public class BackgroundClassification : BaseModel
    {
        public Guid UserId { get; }
        public Guid ImageId { get; set; }
        public string BackgroundCategory { get; set; } = string.Empty;
    }
}