using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RESTfullDemo.Entities;
using RESTfullDemo.Filters;
using RESTfullDemo.Models;
using RESTfullDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfullDemo.Controllers
{
    [Route("api/authors/{authorId}/books")]
    [ApiController]
    [ServiceFilter(typeof(CheckAuthorExistFilterAttribute))]
    public class BookController : ControllerBase
    {
        public BookController(IRepositoryWrapper repositoryWrapper,
            IMapper mapper)
        {
            RepositoryWrapper = repositoryWrapper;
            Mapper = mapper;
        }
        public IRepositoryWrapper RepositoryWrapper { get; }
        public IMapper Mapper { get; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksAsync(Guid authorId)
        {
            var books = await RepositoryWrapper.Book.GetBooksAsync(authorId);
            var bookDtoList = Mapper.Map<IEnumerable<BookDto>>(books);
            return bookDtoList.ToList();
        }

        [HttpGet("{bookId}", Name = nameof(GetBookAsync))]
        public async Task<ActionResult<BookDto>> GetBookAsync(Guid authorId, Guid bookId)
        {
            var targetBook = await RepositoryWrapper.Book.GetBookAsync(authorId, bookId);
            if (targetBook == null)
            {
                return NotFound();
            }
            var bookDto = Mapper.Map<BookDto>(targetBook);
            return bookDto;
        }

        [HttpPost]
        public async Task<IActionResult> AddBookAsync(Guid authorId, BookForCreationDto bookForCreationDto)
        {
            var book = Mapper.Map<Book>(bookForCreationDto);
            book.AuthorId = authorId;
            RepositoryWrapper.Book.Create(book);
            if(!await RepositoryWrapper.Book.SaveAsync())
            {
                throw new Exception("创建资源Book失败");
            }
            var bookDto = Mapper.Map<BookDto>(book);
            return CreatedAtAction(nameof(GetBookAsync), new { authorId = authorId, bookId = bookDto.Id }, bookDto);
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBook(Guid authorId, Guid bookId)
        {
            var book = await RepositoryWrapper.Book.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
            }
            RepositoryWrapper.Book.Delete(book);

            return NoContent();
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBookAsync(Guid authorId, Guid bookId, BookForUpdateDto updatedBook)
        {
            var book = await RepositoryWrapper.Book.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
            }
            Mapper.Map(updatedBook, book, typeof(BookForUpdateDto), typeof(Book));
            RepositoryWrapper.Book.Update(book);
            if(!await RepositoryWrapper.Book.SaveAsync())
            {
                throw new Exception("更新资源Book失败");
            }

            return NoContent();
        }

        [HttpPatch("{bookId}")]
        public async Task<IActionResult> PartiallyUpdateBookAsync(Guid authorId, Guid bookId, JsonPatchDocument<BookForUpdateDto> patchDocument)
        {
            var book = await RepositoryWrapper.Book.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
            }
            var bookUpdateDto = Mapper.Map<BookForUpdateDto>(book);
            patchDocument.ApplyTo(bookUpdateDto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Mapper.Map(bookUpdateDto, book, typeof(BookForUpdateDto), typeof(Book));
            RepositoryWrapper.Book.Update(book);
            if(!await RepositoryWrapper.Book.SaveAsync())
            {
                throw new Exception("更新资源Book失败");
            }

            return NoContent();
        }
    }
}
