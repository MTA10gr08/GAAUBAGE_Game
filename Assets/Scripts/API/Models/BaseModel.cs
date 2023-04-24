using System;
namespace GAAUBAGE_Game.API.Models
{
    public class BaseModel
    {
        /// <summary>
        /// Unique identifier for the object. This property is read-only.
        /// </summary>
        public Guid Id { get; private set; }
        /// <summary>
        /// Creation date of the object. This property is read-only.
        /// </summary>
        public DateTime Created { get; private set; } = DateTime.Now;
        /// <summary>
        /// Updated date of the object. This property is read-only.
        /// </summary>
        public DateTime Updated { get; private set; } = DateTime.Now;
    }
}