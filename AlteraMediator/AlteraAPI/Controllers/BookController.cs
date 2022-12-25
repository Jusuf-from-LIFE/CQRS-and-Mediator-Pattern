using AlteraMediator.Commands;
using AlteraMediator.Models;
using AlteraMediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AlteraAPI.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<List<Book>> Create(List<Book> booksToCreate)
        {
            return await _mediator.Send(new CreateBookCommand(booksToCreate));
        }

        [HttpGet]
        public async Task<List<Book>> Get()
        {
            return await _mediator.Send(new GetBookListQuery());
        }

        [HttpGet("{id}")]
        public async Task<Book> GetById(int id)
        {
            return await _mediator.Send(new GetBookQuery(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string newDescription)
        {
            var book = await _mediator.Send(new UpdateBookCommand(id, newDescription));

            if (book is null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = _mediator.Send(new DeleteBookCommand(id));

            if(result is null)
            {
                return BadRequest();
            }

            return Ok(result.Result);
        }
    }
}
