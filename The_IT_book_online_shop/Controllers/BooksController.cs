using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using The_IT_book_online_shop.Models;

namespace The_IT_book_online_shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public BooksController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            const string apiUrl = "https://api.itbook.store/1.0/search/mysql";

            try
            {
                var response = await _httpClient.GetStringAsync(apiUrl);

                var apiResponse = JsonSerializer.Deserialize<BookApiResponse>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (apiResponse?.Books == null || !apiResponse.Books.Any())
                    return NotFound("No books found.");

                var sortedBooks = apiResponse.Books.OrderBy(b => b.Title).ToList();
                return Ok(sortedBooks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}