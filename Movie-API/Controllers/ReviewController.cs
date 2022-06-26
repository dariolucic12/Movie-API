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
    public class ReviewController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepo _reviewRepo;
        private readonly ILoggerManager _logger;

        public ReviewController(IMapper mapper, IReviewRepo reviewRepo, ILoggerManager logger)
        {
            _mapper = mapper;
            _reviewRepo = reviewRepo;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "ReviewOfUser")]
        public async Task<IActionResult> GetReviewsOfUser(string id)
        {
            try
            {
                var reviews = await _reviewRepo.GetReviewsOfUser(id);

                if (reviews == null)
                {
                    _logger.LogError($"Reviews of user with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned reviews of user with id: {id}");
                    var reviewsResult = _mapper.Map<IEnumerable<ReviewDTO>>(reviews);
                    return Ok(reviewsResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetReviewsOfUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("movieReview/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetReview(int id)
        {
            try
            {

                var reviews = await _reviewRepo.GetReview(id);

                if (reviews == null)
                {
                    _logger.LogError($"Review with id: {id} hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned review with id: {id}");
                    var reviewResult = _mapper.Map<ReviewDTO>(reviews);
                    return Ok(reviewResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetReview action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewDTO reviewDTO)
        {
            try
            {
                if (reviewDTO == null)
                {
                    _logger.LogError("Review object sent from client is null.");
                    return BadRequest("Review object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid review object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var reviewEntity = _mapper.Map<Review>(reviewDTO);

                await _reviewRepo.AddReview(reviewEntity);
                _logger.LogInfo($"Added review of user with id: {reviewEntity.UserId}");

                var createdReview = _mapper.Map<ReviewDTO>(reviewEntity);

                return CreatedAtRoute("ReviewOfUser", new { id = createdReview.Id }, createdReview);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside AddReview action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReview(ReviewDTO review)
        {
            try
            {
                if (review == null)
                {
                    _logger.LogError($"Review info not found in db.");
                    return NotFound();
                }
                var reviewEntity = _mapper.Map<Review>(review);
                await _reviewRepo.DeleteReview(reviewEntity);
                _logger.LogInfo($"Deleted review with movie id: {review.MovieId} of user {review.UserId}");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteReview action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReview([FromBody] ReviewDTO reviewDTO)
        {
            try
            {
                if (reviewDTO == null)
                {
                    _logger.LogError("Review object sent from client is null.");
                    return BadRequest("Review object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid review object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var review = _mapper.Map<Review>(reviewDTO);

                var reviewEntity = await _reviewRepo.GetReview(review.Id);
                if (reviewEntity == null)
                {
                    _logger.LogError($"Review of user with id: {reviewDTO.MovieId}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(reviewDTO, reviewEntity);

                await _reviewRepo.UpdateReview(reviewEntity);
                _logger.LogInfo($"Review of user with id: {reviewDTO.UserId} updated");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateReview action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
