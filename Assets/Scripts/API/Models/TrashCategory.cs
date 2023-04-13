using System;
namespace GAAUBAGE_Game.API.Models
{
    public class TrashCategory : BaseModel
    {
        public Guid UserId { get; set; }
        public Guid TrashSuperCategoryId { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}