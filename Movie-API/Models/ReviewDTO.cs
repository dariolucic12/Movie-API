using System.ComponentModel.DataAnnotations;

namespace Movie_API.Models
{
    public class ReviewDTO
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string MovieId { get; set; }

        public int? Rating { get; set; }

        public string? Comment { get; set; }

        public string FullTitle { get; set; }

        public string Image { get; set; }

        public string IMDbRating { get; set; }

        public string IMDbRatingCount { get; set; }
    }
}
