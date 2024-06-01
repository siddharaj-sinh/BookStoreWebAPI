using BookStoreWebAPI.Models;
using BookStoreWebAPI.Services.Contracts;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreWebAPI.Services.Implementations
{
    public class BookService : IBooksService
    {
        private readonly IMongoCollection<Book> _booksCollection;
        public BookService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings) {

            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);


            _booksCollection = mongoDatabase.GetCollection<Book>(bookStoreDatabaseSettings.Value.BooksCollectionName);

        }
        public async Task<Book?> CreateBookAsync(Book book)
        {
            await _booksCollection.InsertOneAsync(book);
            return book;
        }

        public async Task<Book> DeleteBookAsync(string id)
        {
            var book = await _booksCollection.FindOneAndDeleteAsync(b=>b.Id==id);
            return book;
        }

        public async Task<Book?> GetBookByIdAsync(string id)
        {
            var book = await _booksCollection.FindAsync(b=>b.Id == id);

            return book.FirstOrDefault();
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            return await _booksCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Book?> UpdateBookAsync(Book book)
        {
           var updateResult =  await _booksCollection.ReplaceOneAsync(b=>b.Id==book.Id,book);
            if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
            {
                return book;
            }
            return null;
        }
    }
}
