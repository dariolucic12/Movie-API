using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_API.Models
{
    public class Watchlist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        public string MovieId { get; set; }

        [Required]
        public string FullTitle { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string IMDbRating { get; set; }

        [Required]
        public string IMDbRatingCount { get; set; }
    }
}
