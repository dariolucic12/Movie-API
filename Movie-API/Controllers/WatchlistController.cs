using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_API.Logger;
using Movie_API.Models;
using Movie_API.Repository;

namespace Movie_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWatchlistRepo _watchlistRepo;
        private readonly ILoggerManager _logger;

        public WatchlistController(IMapper mapper, IWatchlistRepo watchlistRepo, ILoggerManager logger)
        {
            _mapper = mapper;
            _watchlistRepo = watchlistRepo;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "WatchlistOfUser")]
        public async Task<IActionResult> GetWatchlistOfUser(string id)
        {
            try
            {
                var watchlist = await _watchlistRepo.GetWatchlistOfUser(id);

                if (watchlist == null)
                {
                    _logger.LogError($"Watchlist of user with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned watchlist of user with id: {id}");
                    var watchlistResult = _mapper.Map<IEnumerable<WatchlistDTO>>(watchlist);
                    return Ok(watchlistResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetWatchlistOfUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToWatchlist([FromBody] WatchlistDTO watchlistDTO)
        {
            try
            {
                if (watchlistDTO == null)
                {
                    _logger.LogError("Watchlist object sent from client is null.");
                    return BadRequest("Watchlist object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid watchlist object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var watchlistEntity = _mapper.Map<Watchlist>(watchlistDTO);

                await _watchlistRepo.AddToWatchlist(watchlistEntity);
                _logger.LogInfo($"Added to watchlist of user with id: {watchlistEntity.UserId}");

                var createdWatchlist = _mapper.Map<WatchlistDTO>(watchlistEntity);

                return CreatedAtRoute("WatchlistOfUser", new { id = createdWatchlist.UserID }, createdWatchlist);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside AddToWatchlist action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFromWatchlist(Watchlist watchlist)
        {
            try
            {
                if (watchlist == null)
                {
                    _logger.LogError($"Watchlist info not found in db.");
                    return NotFound();
                }
                await _watchlistRepo.DeleteFromWatchlist(watchlist);
                _logger.LogInfo($"Deleted movie with id: {watchlist.MovieId} of user {watchlist.UserId}");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteFromWatchlist action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
