
using Application.AuthorCommands.AddAuthorCommand;
using Application.AuthorQueries.GetAllAuthors;
using Application.AuthorQueries.GetAuthorById;
using Application.AuthorCommands.DeleteAuthorCommand;
using Application.AuthorCommands.UpdateAuthorCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        internal readonly IMediator _mediator;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(IMediator mediator, ILogger<AuthorController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllAuthors")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
        {
            try
            {
                var allAuthors = await _mediator.Send(new GetAllAuthorsQuery());
                return Ok(allAuthors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all authors");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet]
        [Route("GetAuthorById/{id}")]
        public async Task<ActionResult<Author>> GetAuthorById(Guid id)
        {
            try
            {
                var getAuthorById = await _mediator.Send(new GetAuthorByIdQuery(id));
                return Ok(getAuthorById);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching author by ID: {Id}", id);
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPost]
        [Route("AddAuthorCommand")]
        public async Task<ActionResult<Author>> AddAuthorCommand([FromBody] AddAuthorCommand command)
        {
            try
            {
                var authorToAdd = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetAuthorById), new { id = authorToAdd.Id }, authorToAdd);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new author");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpDelete]
        [Route("DeleteAuthorCommand/{id}")]
        public async Task<ActionResult> DeleteAuthorCommand(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new { error = "Invalid author ID" });
            }

            try
            {
                var result = await _mediator.Send(new DeleteAuthorCommand(id));
                if (result == null)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Attempted to delete non-existent author with ID: {Id}", id);
                return NotFound(new { error = "Author not found" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting author: {Id}", id);
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Author>> UpdateAuthorCommand(Guid id, [FromBody] UpdateAuthorCommand request)
        {
            if (id == Guid.Empty || !ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var command = new UpdateAuthorCommand(id, request.UpdatedName);
                var updatedAuthor = await _mediator.Send(command);
                return Ok(updatedAuthor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating author: {Id}", id);
                return StatusCode(500, new { error = "Internal server error" });
            }
        }
    }
}
