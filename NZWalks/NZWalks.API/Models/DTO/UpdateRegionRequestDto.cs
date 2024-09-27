using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Code has to be a minimum of 1 characters")]
        [MaxLength(5, ErrorMessage = "Code has to be a maximum of 5 characters")]
        public string Code { get; set; } = "";
        [Required]
        [MaxLength(20, ErrorMessage = "Code has to be a maximum of 20 characters")]
        public string Name { get; set; } = "";
        public string? RegionImageUrl { get; set; }
    }
}
