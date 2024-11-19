using Application.BookQueries.GetAllBooks;
using Application.BookQueries.GetBookById;
using Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly FakeDatabase _fakeDatabase;


        public BookController(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var query = new GetAllBooksQuery();
            var handler = new GetAllBooksQueryHandler(_fakeDatabase);
            var books = handler.Handle(query);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var query = new GetBookByIdQuery { Id = id };
            var handler = new GetBookByIdQueryHandler(_fakeDatabase);
            var book = handler.Handle(query);
            if (book == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }
            return Ok(book);
        }
    }
}
