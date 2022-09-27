namespace Movie_API.Models
{
    public class WatchlistDTO
    {
        public int Id { get; set; }

        public string UserID { get; set; }

        public string MovieId { get; set; }

        public string FullTitle { get; set; }

        public string Image { get; set; }

        public string IMDbRating { get; set; }

        public string IMDbRatingCount { get; set; }
    }
}
