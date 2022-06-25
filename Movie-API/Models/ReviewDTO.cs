using System.ComponentModel.DataAnnotations;

namespace Movie_API.Models
{
    public class ReviewDTO
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string MovieId { get; set; }

        public int? Rating { get; set; }

        public string? Comment { get; set; }
    }
}
