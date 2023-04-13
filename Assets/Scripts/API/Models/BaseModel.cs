using System;
namespace GAAUBAGE_Game.API.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}