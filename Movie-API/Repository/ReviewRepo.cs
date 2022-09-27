using Microsoft.EntityFrameworkCore;
using Movie_API.Models;

namespace Movie_API.Repository
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly ApplicationContext _context;
        public ReviewRepo(ApplicationContext context)
        {
            _context = context; 
        }

        public async Task AddReview(Review review)
        {
            if (_context != null)
            {
                await _context.Reviews.AddAsync(review);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteReview(Review review)
        {
            if (_context != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Review> GetReview(int id)
        {
            if (_context != null)
            {
                return await _context.Reviews.Where(rev => rev.Id.Equals(id)).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<List<Review>> GetReviewsOfUser(string id)
        {
            if (_context != null)
            {
                return await _context.Reviews.Where(review => review.UserId.Equals(id)).ToListAsync();
            }
            return null;
        }

        public async Task UpdateReview(Review review)
        {
            if (_context != null)
            {
                _context.Reviews.Update(review);
                await _context.SaveChangesAsync();
            }
        }
    }
}
