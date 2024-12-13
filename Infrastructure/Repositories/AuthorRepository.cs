
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDatabase _database;

        public AuthorRepository(LibraryDatabase database)
        {
            _database = database;
        }

        public async Task<Author> AddAuthor(Author author)
        {
            await _database.Authors.AddAsync(author); // Använd `AddAsync` för asynkron operation.
            await _database.SaveChangesAsync();
            return author;
        }

        public async Task<Author> DeleteAuthor(Guid Id)
        {
            Author authorToDelete = _database.Authors.FirstOrDefault(a => a.Id == Id);

            // Kontrollera om författaren finns
            if (authorToDelete == null)
            {
                throw new InvalidOperationException($"Author with ID {Id} not found.");
            }

            // Ta bort författaren
            _database.Authors.Remove(authorToDelete);
            await _database.SaveChangesAsync();

            // Returnera den borttagna författaren
            return authorToDelete;
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _database.Authors.ToListAsync(); // Returnerar alla författare som en lista.
        }

        public async Task<Author> GetAuthorById(Guid id)
        {
            return await _database.Authors.FindAsync(id); // Hämtar författare baserat på ID.
        }

        public async Task<Author> UpdateAuthor(Guid id, Author updatedAuthor)
        {
            var authorToUpdate = await _database.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (authorToUpdate != null)
            {
                // Uppdatera endast fält som är ändrade.
                authorToUpdate.Name = updatedAuthor.Name;

                _database.Authors.Update(authorToUpdate); // Markera objektet som ändrat.
                await _database.SaveChangesAsync(); // Spara ändringar till databasen.
                return authorToUpdate;
            }
            return null; // Om författaren inte hittades.
        }
    }
}
