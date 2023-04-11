
using System;
namespace API.DTOs.Annotation
{
    public class BackgroundClassificationDTO : BaseDTO
    {
        public Guid UserId { get; }
        public Guid ImageId { get; set; }
        public string BackgroundCategory { get; set; } = string.Empty;
    }
}