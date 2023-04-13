using System;
namespace GAAUBAGE_Game.API.Models
{
    public class TrashSuperCategory : BaseModel
    {
        public Guid UserId { get; set; }
        public Guid TrashBoundingBoxId { get; set; }
        public string SuperCategory { get; set; } = string.Empty;
    }
}