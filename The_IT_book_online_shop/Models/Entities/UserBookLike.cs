namespace The_IT_book_online_shop.Models.Entities
{
    public class UserBookLike
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string Isbn13 { get; set; }

        public User User { get; set; }
    }
}
