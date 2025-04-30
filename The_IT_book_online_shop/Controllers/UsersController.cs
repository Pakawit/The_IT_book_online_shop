using Microsoft.AspNetCore.Mvc;
using The_IT_book_online_shop.Data;
using The_IT_book_online_shop.Models;
using The_IT_book_online_shop.Models.Entities;

namespace The_IT_book_online_shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public UsersController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/users
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _dbContext.Users.ToList();
            return Ok(users);
        }

        // POST: api/users/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] AddUserDto newUser)
        {
            //ตรวจสอบว่า UserName ที่ผู้ใช้กรอกมีอยู่ในฐานข้อมูลหรือไม่
            if (_dbContext.Users.Any(u => u.UserName == newUser.UserName))
            {
                return BadRequest("Username is already taken.");
            }
            //เข้ารหัสรหัสผ่าน
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newUser.PassWord);
            var user = new User
            {
                FullName = newUser.FullName,
                UserName = newUser.UserName,
                PassWord = hashedPassword
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return Ok(new
            {
                message = "User registered successfully.",
                user = new { user.UserId, user.FullName, user.UserName }
            });
        }

        // POST: api/users/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            //ค้นหาผู้ใช้ที่มีชื่อผู้ใช้ตรงกับที่ผู้ใช้กรอก
            var user = _dbContext.Users.FirstOrDefault(u => u.UserName == login.UserName);

            //ตรวจสอบว่าผู้ใช้ที่พบมีรหัสผ่านที่ถูกต้อง
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.PassWord, user.PassWord))
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(new
            {
                message = "Login successful.",
                user = new { user.UserId, user.FullName, user.UserName }
            });
        }
    }
}