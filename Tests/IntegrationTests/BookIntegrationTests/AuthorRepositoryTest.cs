using FakeItEasy;
using MediatR;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories;

namespace Tests.IntegrationTests.BookIntegrationTests
{
    public class AuthorRepositoryTest : IDisposable
    {
        private readonly LibraryDatabase _database;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMediator _mediator;

        public AuthorRepositoryTest()
        {
            
            var options = new DbContextOptionsBuilder<LibraryDatabase>()
                .UseInMemoryDatabase("LibraryTestDb_Author")
                .Options;

            _database = new LibraryDatabase(options);
            _authorRepository = new AuthorRepository(_database);
            _mediator = A.Fake<IMediator>();
        }

        public void Dispose()
        {
            _database.Database.EnsureDeleted();
            _database.Dispose();
        }

        [Fact]
        public async Task AddAuthor_ShouldAddAuthorToDatabase()
        {
            var author = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Test Author"
            };

            var addedAuthor = await _authorRepository.AddAuthor(author);

            var authorFromDb = await _database.Authors.FindAsync(addedAuthor.Id);

            Assert.NotNull(authorFromDb);
            Assert.Equal(author.Name, authorFromDb.Name);
        }

        [Fact]
        public async Task GetAllAuthors_ShouldReturnAuthors()
        {
            var author1 = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Author 1"
            };
            var author2 = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Author 2"
            };

            await _authorRepository.AddAuthor(author1);
            await _authorRepository.AddAuthor(author2);

            var authors = await _authorRepository.GetAllAuthors();

            Assert.Equal(2, authors.Count);
            Assert.Contains(authors, a => a.Name == author1.Name);
            Assert.Contains(authors, a => a.Name == author2.Name);
        }

        [Fact]
        public async Task GetAuthorById_ShouldReturnCorrectAuthor()
        {
            var author = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Test Author"
            };

            var addedAuthor = await _authorRepository.AddAuthor(author);
            var authorFromDb = await _authorRepository.GetAuthorById(addedAuthor.Id);

            Assert.NotNull(authorFromDb);
            Assert.Equal(addedAuthor.Id, authorFromDb.Id);
        }

        [Fact]
        public async Task UpdateAuthor_ShouldUpdateAuthorInDatabase()
        {
            var author = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Original Author"
            };

            var addedAuthor = await _authorRepository.AddAuthor(author);

            var updatedAuthor = new Author
            {
                Name = "Updated Author"
            };

            var authorFromDb = await _authorRepository.UpdateAuthor(addedAuthor.Id, updatedAuthor);

            Assert.NotNull(authorFromDb);
            Assert.Equal(updatedAuthor.Name, authorFromDb.Name);
        }

        [Fact]
        public async Task DeleteAuthor_ShouldRemoveAuthorFromDatabase()
        {
            var author = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Test Author"
            };

            var addedAuthor = await _authorRepository.AddAuthor(author);

            var deletedAuthor = await _authorRepository.DeleteAuthor(addedAuthor.Id);

            Assert.NotNull(deletedAuthor);
            Assert.Equal(addedAuthor.Id, deletedAuthor.Id);

            var authorFromDb = await _database.Authors.FindAsync(addedAuthor.Id);
            Assert.Null(authorFromDb);
        }

        [Fact]
        public async Task DeleteAuthor_ShouldThrowExceptionIfNotFound()
        {
            var nonExistingId = Guid.NewGuid();

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _authorRepository.DeleteAuthor(nonExistingId));

            Assert.Equal($"Author with ID {nonExistingId} not found.", exception.Message);
        }

    }
}
