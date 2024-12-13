using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IAuthorRepository
    {
        Task<Author> AddAuthor(Author author);
        Task<List<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(Guid id);
        Task<Author> UpdateAuthor(Guid id, Author author);
        Task<Author> DeleteAuthor(Guid Id);

    }
}
