using System;
namespace API.DTOs
{
    public class BaseDTO
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}