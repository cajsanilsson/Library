using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface.RepositoryInterfaces;
using Domain.Models;
using Infrastructure.Database;

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
            _database.Authors.Add(author);
            _database.SaveChanges();
            return author;
        }

        public Task<Author> DeleteAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public Task<List<Author>> GetAllAuthors()
        {
            throw new NotImplementedException();
        }

        public Task<Author> GetAuthorById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Author> UpdateAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public Task<Author> UpdateAuthor(Guid id, Author author)
        {
            throw new NotImplementedException();
        }
    }
}
