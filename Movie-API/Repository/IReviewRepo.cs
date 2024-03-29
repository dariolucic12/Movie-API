﻿using Movie_API.Models;

namespace Movie_API.Repository
{
    public interface IReviewRepo
    {
        Task<List<Review>> GetReviewsOfUser(string id);

        Task<Review> GetReview(int id);

        Task AddReview(Review review);

        Task DeleteReview(Review review);

        Task UpdateReview(Review review);
    }
}
