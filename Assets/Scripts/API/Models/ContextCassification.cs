using System;
namespace GAAUBAGE_Game.API.Models
{
    public class ContextClassification : BaseModel
    {
        public Guid UserId { get; set; }
        public Guid BackgroundClassificationId { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}