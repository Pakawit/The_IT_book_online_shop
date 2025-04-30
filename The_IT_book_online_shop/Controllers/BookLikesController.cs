using Microsoft.AspNetCore.Mvc;
using The_IT_book_online_shop.Data;
using The_IT_book_online_shop.Models;
using The_IT_book_online_shop.Models.Entities;

namespace The_IT_book_online_shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookLikesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public BookLikesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/booklikes
        [HttpGet]
        public IActionResult GetAllBookLikes()
        {
            var likes = _dbContext.UserBookLikes
                .Select(l => new
                {
                    l.Id,
                    l.UserId,
                    l.Isbn13
                })
                .ToList();

            return Ok(likes);
        }

        // POST: api/booklikes/like
        [HttpPost("like")]
        public IActionResult LikeBook([FromBody] LikeBookDto likeDto)
        {
            if (likeDto == null)
                return BadRequest(new { message = "Invalid data." });

            // ตรวจสอบว่าผู้ใช้มีอยู่จริง
            var userExists = _dbContext.Users.Any(u => u.UserId == likeDto.UserId);
            if (!userExists)
            {
                return NotFound(new { message = "User not found." });
            }

            // ตรวจสอบว่าเคยกด like ไปแล้วหรือยัง
            bool alreadyLiked = _dbContext.UserBookLikes.Any(l => l.UserId == likeDto.UserId && l.Isbn13 == likeDto.Isbn13);

            if (alreadyLiked)
            {
                return BadRequest(new { message = "User already liked this book." });
            }

            // เพิ่ม Like
            var like = new UserBookLike
            {
                UserId = likeDto.UserId,
                Isbn13 = likeDto.Isbn13
            };

            _dbContext.UserBookLikes.Add(like);
            _dbContext.SaveChanges();

            return Ok(new { message = "Book liked successfully." });
        }
    }
}