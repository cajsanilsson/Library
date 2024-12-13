using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDatabase _database;

        public BookRepository(LibraryDatabase database)
        {
            _database = database;
        }

        public async Task<Book> AddBook(Book book)
        {
            await _database.Books.AddAsync(book);
            await _database.SaveChangesAsync();
            return book;
        }

        public async Task<Book> DeleteBook(Guid id)
        {
            Book bookToDelete = _database.Books.FirstOrDefault(a => a.Id == id);

            _database.Books.Remove(bookToDelete);
            await _database.SaveChangesAsync();

            return bookToDelete;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _database.Books.ToListAsync();
        }

        public async Task<Book> GetBookById(Guid id)
        {
            return await _database.Books.FindAsync(id);
        }

        public async Task<Book> UpdateBook(Guid id, Book updatedBook)
        {
            var bookToUpdate = await _database.Books.FirstOrDefaultAsync(a => a.Id == id);
            if (bookToUpdate != null)
            {
              
                bookToUpdate.Title = updatedBook.Title;
                bookToUpdate.Description = updatedBook.Description;

                _database.Books.Update(bookToUpdate); 
                await _database.SaveChangesAsync(); 
                return bookToUpdate;
            }
            return null;
        }
    }
}
