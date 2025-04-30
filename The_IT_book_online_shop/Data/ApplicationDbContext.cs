using Microsoft.EntityFrameworkCore;
using The_IT_book_online_shop.Models.Entities;

namespace The_IT_book_online_shop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserBookLike> UserBookLikes { get; set; }

    }
}
