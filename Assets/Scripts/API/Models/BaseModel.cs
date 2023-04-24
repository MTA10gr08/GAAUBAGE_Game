using System;
namespace GAAUBAGE_Game.API.Models
{
    public class BaseModel
    {
        /// <summary>
        /// Unique identifier for the object. This property is read-only.
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// Creation date of the object. This property is read-only.
        /// </summary>
        public DateTime Created { get; set; } = DateTime.Now;
        /// <summary>
        /// Updated date of the object. This property is read-only.
        /// </summary>
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}