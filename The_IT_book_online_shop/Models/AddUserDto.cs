namespace The_IT_book_online_shop.Models
{
    public class AddUserDto
    {
        public required string UserName { get; set; }

        public required string PassWord { get; set; }
        public required string FullName { get; set; }
    }
}
