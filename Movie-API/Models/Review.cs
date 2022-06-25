using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_API.Models
{
    public class Review
    {
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        public string MovieId { get; set; }

        public int? Rating { get; set; }

        public string? Comment { get; set; }
    }
}
