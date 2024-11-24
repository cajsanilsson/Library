using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.BookQueries.GetAllBooks
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<Book>>
    {
        private readonly FakeDatabase _fakeDatabase;
        private readonly ILogger<GetAllBooksQueryHandler> _logger;

        public GetAllBooksQueryHandler(FakeDatabase fakeDatabase, ILogger<GetAllBooksQueryHandler> logger)
        {
            _fakeDatabase = fakeDatabase;
            _logger = logger;
        }

        public Task<List<Book>> Handle(GetAllBooksQuery query, CancellationToken cancellationToken)
        {
            try
            {
                if (_fakeDatabase.books == null)
                {
                    _logger.LogWarning("The book list is null.");
                    return Task.FromResult(new List<Book>());
                }

                _logger.LogInformation("Fetching all books.");

                return Task.FromResult(_fakeDatabase.books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching books.");
                throw new ApplicationException("Error fetching books.", ex);
            }
        }


    }
}
