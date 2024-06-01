using BookStoreWebAPI.Models;

namespace BookStoreWebAPI.Services.Contracts
{
    public interface IBooksService
    {
        Task<List<Book>> GetBooksAsync();

        Task<Book?> GetBookByIdAsync(string id);
        Task<Book?> CreateBookAsync(Book book);

        Task<Book?> UpdateBookAsync(Book book);

        Task<Book?> DeleteBookAsync(string id);

    }
}
