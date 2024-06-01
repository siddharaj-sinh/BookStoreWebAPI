using BookStoreWebAPI.Models;
using BookStoreWebAPI.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;
        public BooksController(IBooksService booksService)
        {

            _booksService = booksService;

        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> Get()
        {
            var books = await _booksService.GetBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Book>> Get(string id)
        {
            var book = await _booksService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Post(Book book)
        {
            await _booksService.CreateBookAsync(book);
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, Book updatedBook)
        {
            if (id != updatedBook.Id)
            {
                return BadRequest();
            }

            var book = await _booksService.UpdateBookAsync(updatedBook);
            if (book == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _booksService.DeleteBookAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
