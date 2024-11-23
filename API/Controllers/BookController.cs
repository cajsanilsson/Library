using Application.BookQueries.GetAllBooks;
using Application.BookQueries.GetBookById;
using Application.BookCommands.AddBookCommand;
using Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Application.BookCommands.DeleteBookCommand;
using Application.BookCommands.UpdateBookCommand;
using Application.AuthorCommands.AddAuthorCommand;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        internal readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Route("GetAllBooks")]
        public async Task <IActionResult> GetAllBooks()
        {
           return Ok (await _mediator.Send(new GetAllBooksQuery()));
        }

        [HttpGet]
        [Route("GetBookById/{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            return Ok(await _mediator.Send(new  GetBookByIdQuery(id)));
        }

        [HttpPost]
        [Route("AddBookCommand")]

        public async void AddBookCommand([FromBody] Book newBook)
        {
            await _mediator.Send(new AddBookCommand(newBook));
        }

        [HttpDelete]
        [Route("DeleteBookCommand{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteBookCommand(id));
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateBookCommand{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookCommand request)
        {
            try
            {
                var command = new UpdateBookCommand(id, request.NewTitle, request.NewDescription);
                var updatedBook = await _mediator.Send(command);
                return Ok(updatedBook);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
