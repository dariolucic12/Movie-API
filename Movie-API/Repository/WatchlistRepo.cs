using Microsoft.EntityFrameworkCore;
using Movie_API.Models;

namespace Movie_API.Repository
{
    public class WatchlistRepo : IWatchlistRepo
    {
        private readonly ApplicationContext _context;

        public WatchlistRepo(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddToWatchlist(Watchlist watchlist)
        {
            if (_context != null)
            {
                await _context.Watchlists.AddAsync(watchlist);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteFromWatchlist(Watchlist watchlist)
        {
            if (_context != null)
            {
                //var deletedMovie = _context.Watchlists.Where(watch => watch.UserId.Equals(watchlist.UserId) && watch.MovieId.Equals(watchlist.MovieId));
                //_context.Watchlists.Remove((Watchlist)deletedMovie);
                _context.Watchlists.Remove(watchlist);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Watchlist>> GetWatchlistOfUser(string id)
        {
            if (_context != null)
            {
                return await _context.Watchlists.Where(watchlist => watchlist.UserId.Equals(id)).ToListAsync();
            }
            return null;
        }

        public async Task<Watchlist> GetWatchlist(int id)
        {
            if (_context != null)
            {
                return await _context.Watchlists.Where(watchlist => watchlist.Id.Equals(id)).FirstOrDefaultAsync();
            }
            return null;
        }
    }
}
