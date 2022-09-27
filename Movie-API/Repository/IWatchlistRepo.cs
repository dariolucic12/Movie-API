using Movie_API.Models;

namespace Movie_API.Repository
{
    public interface IWatchlistRepo
    {
        Task<List<Watchlist>> GetWatchlistOfUser(string id); //vraca se lista userovih filmova na njoj

        Task<Watchlist> GetWatchlist(int id); //dohvaca jednu watchlistu

        Task AddToWatchlist(Watchlist watchlist); //dobije se WatchlistDTO i dodaje film na listu usera

        Task DeleteFromWatchlist(Watchlist watchlist); //dobije se WatchlistDTO i brise film od usera
    }
}
