using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RESTfullDemo.Entities;
using RESTfullDemo.Helpers;
using RESTfullDemo.Models;
using RESTfullDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfullDemo.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        public AuthorController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            RepositoryWrapper = repositoryWrapper;
            Mapper = mapper;
        }
        public IRepositoryWrapper RepositoryWrapper { get; }
        public IMapper Mapper { get; }

        [HttpGet(Name = nameof(GetAuthorsAsync))]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthorsAsync([FromQuery] AuthorResourceParameters parameters)
        {
            var pagedList = await RepositoryWrapper.Author.GetAllAsync(parameters);
            var paginationMetadata = new
            {
                totalCount = pagedList.TotalCount,
                pageSize = pagedList.PageSize,
                currentPage = pagedList.CurrentPage,
                totalPages = pagedList.TotalPages,
                previousePageLink = pagedList.HasPrevious ? Url.Link(nameof(GetAuthorsAsync), new
                {
                    pageNumber = pagedList.CurrentPage - 1,
                    pageSize = pagedList.PageSize,
                    birthPlace = parameters.BirthPlace,
                    searchQuery = parameters.SearchQuery,
                    sortBy = parameters.SortBy
                }) : null,
                nextPageLink = pagedList.HasNext ? Url.Link(nameof(GetAuthorsAsync), new
                {
                    pageNumber = pagedList.CurrentPage + 1,
                    pageSize = pagedList.PageSize,
                    birthPlace = parameters.BirthPlace,
                    searchQuery = parameters.SearchQuery,
                    sortBy = parameters.SortBy
                }) : null
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));

            var authorDtoList = Mapper.Map<IEnumerable<AuthorDto>>(pagedList);
            return authorDtoList.ToList();

        }

        [HttpGet("{authorId}", Name = nameof(GetAuthorAsync))]
        public async Task<ActionResult<AuthorDto>> GetAuthorAsync(Guid authorId)
        {
            var author = await RepositoryWrapper.Author.GetByIdAsync(authorId);
            if (author == null)
            {
                return NotFound();
            }
            else
            {
                return Mapper.Map<AuthorDto>(author);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthorAsync(AuthorForCreationDto authorForCreationDto)
        {
            var author = Mapper.Map<Author>(authorForCreationDto);

            RepositoryWrapper.Author.Create(author);
            var result = await RepositoryWrapper.Author.SaveAsync();
            if (!result)
            {
                throw new Exception("创建资源author失败");
            }
            var authorCreated = Mapper.Map<AuthorDto>(author);
            return CreatedAtRoute(nameof(GetAuthorAsync),
                new { authorId = authorCreated.Id },
                authorCreated);
        }

        [HttpDelete("{authorId}")]
        public async Task<IActionResult> DeleteAuthorAsync(Guid authorId)
        {
            var author = await RepositoryWrapper.Author.GetByIdAsync(authorId);
            if (author == null)
            {
                return NotFound();
            }
            RepositoryWrapper.Author.Delete(author);
            var result = await RepositoryWrapper.Author.SaveAsync();
            if (!result)
            {
                throw new Exception("删除资源author失败");
            }
            return NoContent();
        }
    }
}
