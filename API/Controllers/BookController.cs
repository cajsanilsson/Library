using Application.BookQueries.GetAllBooks;
using Application.BookQueries.GetBookById;
using Application.BookCommands.AddBookCommand;
using Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.BookCommands.DeleteBookCommand;
using Application.BookCommands.UpdateBookCommand;
using Domain.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        internal readonly IMediator _mediator;
        private readonly ILogger<BookController> _logger;

        public BookController(IMediator mediator, ILogger<BookController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet]
        [Route("GetAllBooks")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            try
            {
                var allBooks = await _mediator.Send(new GetAllBooksQuery());
                return Ok(allBooks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all books");
                return StatusCode(500, new { error = "Internal server error" });
            }
           
        }

        [HttpGet]
        [Route("GetBookById/{id}")]
        public async Task<ActionResult<Book>> GetBookById(Guid id)
        {
            try
            {
                var getBookById = await _mediator.Send(new GetBookByIdQuery(id));
                return Ok(getBookById);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching book by ID: {Id}", id);
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPost]
        [Route("AddBookCommand")]
        public async Task<ActionResult<Book>> AddBookCommand([FromBody] AddBookCommand command)
        {
            try
            {
                var bookToAdd = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetBookById), new { id = bookToAdd.Id }, bookToAdd);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new book");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpDelete]
        [Route("DeleteBookCommand/{id}")]
        public async Task<ActionResult> DeleteBookCommand(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new { error = "Invalid book ID" });
            }

            try
            {
                var result = await _mediator.Send(new DeleteBookCommand(id));
                if (result == null)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Attempted to delete non-existent book with ID: {Id}", id);
                return NotFound(new { error = "Book not found" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting book: {Id}", id);
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBookCommand(Guid id, [FromBody] UpdateBookCommand request)
        {
            if (id == Guid.Empty || !ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var command = new UpdateBookCommand(id, request.NewTitle, request.NewDescription);
                var updatedBook = await _mediator.Send(command);
                return Ok(updatedBook);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating book: {Id}", id);
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

    }
}
