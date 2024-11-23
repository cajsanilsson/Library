
using Application.AuthorCommands.AddAuthorCommand;
using Application.AuthorQueries.GetAllAuthors;
using Application.AuthorQueries.GetAuthorById;
using Application.AuthorCommands.DeleteAuthorCommand;
using Application.AuthorCommands.UpdateAuthorCommand;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        internal readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Route("GetAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            return Ok(await _mediator.Send(new GetAllAuthorsQuery()));
        }

        [HttpGet]
        [Route("GetAuthorById/{id}")]
        public async Task<IActionResult> GetAuthorById(Guid id)
        {
            return Ok(await _mediator.Send(new GetAuthorByIdQuery(id)));
        }

        [HttpPost]
        [Route("AddAuthorCommand")]
        public async void AddAuthorCommand([FromBody] Author newAuthor)
        {
            await _mediator.Send(new AddAuthorCommand(newAuthor));
        }


        [HttpDelete]
        [Route("DeleteAuthorCommand{id}")]
        public async Task<IActionResult> DeleteAuthorCommand(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteAuthorCommand(id));
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateAuthorCommand/{id}")]
        public async Task<IActionResult> UpdateAuthorCommand(Guid id, [FromBody] UpdateAuthorCommand request)
        {
            try
            {
                var command = new UpdateAuthorCommand(id, request.UpdatedName);
                var updatedAuthor = await _mediator.Send(command);
                return Ok(updatedAuthor);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
