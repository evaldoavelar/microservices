using System.ComponentModel.DataAnnotations;

namespace  PlatformService.DTO
{
    public class PlatformCreatDTO
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Published { get; set; }
        [Required]
        public string? Cost { get; set; }
    }
}