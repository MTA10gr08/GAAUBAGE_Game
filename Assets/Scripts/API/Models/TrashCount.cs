using System;
namespace GAAUBAGE_Game.API.Models
{
    public class TrashCount : BaseModel
    {
        public Guid UserId { get; set; }
        public Guid ContextCassificationId { get; set; }
        public uint Count { get; set; }
    }
}