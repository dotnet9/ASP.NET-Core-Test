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

        // GET: api/authors
        /// <summary>
        /// 获取所有作者信息
        /// </summary>
        /// <param name="parameters">分页查询参数</param>
        /// <returns>作者信息数组</returns>
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

        // GET: api/authors/12a1e011-d33e-4ab2-be98-017c4cd46cac
        /// <summary>
        /// 根据作者ID查询作者基本信息
        /// </summary>
        /// <param name="authorId">作者ID</param>
        /// <returns>作者基本信息</returns>
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

        // POST: api/authors
        /// <summary>
        /// 添加作者
        /// </summary>
        /// <param name="authorForCreationDto">作者基本信息</param>
        /// <returns>添加后的作者基本信息</returns>
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

        // DELETE: api/authors/12a1e011-d33e-4ab2-be98-017c4cd46cac
        /// <summary>
        /// 删除指定ID的作者
        /// </summary>
        /// <param name="authorId">作者ID</param>
        /// <returns></returns>
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
