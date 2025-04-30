namespace The_IT_book_online_shop.Models
{
    public class BookDto
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Isbn13 { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
    }

    public class BookApiResponse
    {
        public string Error { get; set; }
        public string Total { get; set; }
        public string Page { get; set; }
        public List<BookDto> Books { get; set; }
    }
}
