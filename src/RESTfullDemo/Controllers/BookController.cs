using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using RESTfullDemo.Entities;
using RESTfullDemo.Filters;
using RESTfullDemo.Helpers;
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


        // GET: api/authors/12a1e011-d33e-4ab2-be98-017c4cd46cac/books
        /// <summary>
        /// 获取指定Id的作者所有书籍信息
        /// </summary>
        /// <param name="authorId">作者ID</param>
        /// <returns>数据列表</returns>
        [HttpGet(Name =nameof(GetBooksAsync))]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksAsync(Guid authorId)
        {
            var books = await RepositoryWrapper.Book.GetBooksAsync(authorId);
            var bookDtoList = Mapper.Map<IEnumerable<BookDto>>(books);
            return bookDtoList.ToList();
        }

        // GET: api/authors/12a1e011-d33e-4ab2-be98-017c4cd46cac/books/904bc178-a2ee-41dc-c60e-08d819ade7e9
        /// <summary>
        /// 获取指定作者ID的指定书籍Id信息
        /// </summary>
        /// <param name="authorId">作者ID</param>
        /// <param name="bookId">书籍ID</param>
        /// <returns>书籍基本信息</returns>
        [HttpGet("{bookId}", Name = nameof(GetBook))]
        public async Task<ActionResult<BookDto>> GetBook(Guid authorId, Guid bookId)
        {
            var targetBook = await RepositoryWrapper.Book.GetBookAsync(authorId, bookId);
            if (targetBook == null)
            {
                return NotFound();
            }
            var bookDto = Mapper.Map<BookDto>(targetBook);
            return bookDto;
        }

        // POST: api/authors/12a1e011-d33e-4ab2-be98-017c4cd46cac/books
        /// <summary>
        /// 创建书籍
        /// </summary>
        /// <param name="authorId">作者ID</param>
        /// <param name="bookForCreationDto">书籍基本信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddBookAsync(Guid authorId, BookForCreationDto bookForCreationDto)
        {
            var book = Mapper.Map<Book>(bookForCreationDto);
            book.AuthorId = authorId;
            RepositoryWrapper.Book.Create(book);
            if (!await RepositoryWrapper.Book.SaveAsync())
            {
                throw new Exception("创建资源Book失败");
            }
            var bookDto = Mapper.Map<BookDto>(book);
            return CreatedAtAction(nameof(GetBook),
                new { authorId = authorId, bookId = bookDto.Id }, 
                bookDto);
        }

        // DELETE: api/authors/12a1e011-d33e-4ab2-be98-017c4cd46cac/books/904bc178-a2ee-41dc-c60e-08d819ade7e9
        /// <summary>
        /// 删除指定作者Id的指定书籍ID信息
        /// </summary>
        /// <param name="authorId">作者ID</param>
        /// <param name="bookId">书籍ID</param>
        /// <returns></returns>
        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBook(Guid authorId, Guid bookId)
        {
            var book = await RepositoryWrapper.Book.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
            }
            RepositoryWrapper.Book.Delete(book);
            if (!await RepositoryWrapper.Book.SaveAsync())
            {
                throw new Exception("删除资源Book失败");
            }

            return NoContent();
        }

        // UPDATE: api/authors/12a1e011-d33e-4ab2-be98-017c4cd46cac/books/904bc178-a2ee-41dc-c60e-08d819ade7e9
        /// <summary>
        /// 更新书籍
        /// </summary>
        /// <param name="authorId">作者ID</param>
        /// <param name="bookId">书籍ID</param>
        /// <param name="updatedBook">需要更新的书籍信息</param>
        /// <returns></returns>
        [HttpPut("{bookId}")]
        [CheckIfMatchHeaderFilter]
        public async Task<IActionResult> UpdateBookAsync(Guid authorId, Guid bookId, BookForUpdateDto updatedBook)
        {
            var book = await RepositoryWrapper.Book.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
            }

            //var entityHash = HashFactory.GetHash(book);
            //if(Request.Headers.TryGetValue(HeaderNames.IfMatch, out var requestETag)
            //    &&requestETag!=entityHash)
            //{
            //    return StatusCode(StatusCodes.Status412PreconditionFailed);
            //}

            Mapper.Map(updatedBook, book, typeof(BookForUpdateDto), typeof(Book));
            RepositoryWrapper.Book.Update(book);
            if (!await RepositoryWrapper.Book.SaveAsync())
            {
                throw new Exception("更新资源Book失败");
            }

            //var entityNewHash = HashFactory.GetHash(book);
            //Response.Headers[HeaderNames.ETag] = entityNewHash;

            return NoContent();
        }

        // PATCH: api/authors/12a1e011-d33e-4ab2-be98-017c4cd46cac/books/904bc178-a2ee-41dc-c60e-08d819ade7e9
        /// <summary>
        /// 更新部分书籍信息
        /// </summary>
        /// <param name="authorId">作者ID</param>
        /// <param name="bookId">书籍ID</param>
        /// <param name="patchDocument">需要更新的书籍部分信息</param>
        /// <returns></returns>
        [HttpPatch("{bookId}")]
        public async Task<IActionResult> PartiallyUpdateBookAsync(Guid authorId, Guid bookId, JsonPatchDocument<BookForUpdateDto> patchDocument)
        {
            var book = await RepositoryWrapper.Book.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
            }

            //var entityHash = HashFactory.GetHash(book);
            //if (Request.Headers.TryGetValue(HeaderNames.IfMatch, out var requestETag)
            //    && requestETag != entityHash)
            //{
            //    return StatusCode(StatusCodes.Status412PreconditionFailed);
            //}

            var bookUpdateDto = Mapper.Map<BookForUpdateDto>(book);
            patchDocument.ApplyTo(bookUpdateDto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Mapper.Map(bookUpdateDto, book, typeof(BookForUpdateDto), typeof(Book));
            RepositoryWrapper.Book.Update(book);
            if (!await RepositoryWrapper.Book.SaveAsync())
            {
                throw new Exception("更新资源Book失败");
            }

            //var entityNewHash = HashFactory.GetHash(book);
            //Response.Headers[HeaderNames.ETag] = entityNewHash;

            return NoContent();
        }
    }
}
