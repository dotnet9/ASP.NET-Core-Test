using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTfullDemo.Models;
using System.Collections.Generic;

namespace RESTfullDemo.Controllers
{
    [Route("api")]
    [ApiController]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var links = new List<Link>();
            links.Add(new Link(HttpMethods.Get,
                "self",
                Url.Link(nameof(GetRoot), null)));

            links.Add(new Link(HttpMethods.Get,
                "get authors",
                Url.Link(nameof(AuthorController.GetAuthorsAsync), null)));

            links.Add(new Link(HttpMethods.Post,
                "create author",
                Url.Link(nameof(AuthorController.CreateAuthorAsync), null)));

            return Ok(links);
        }
    }
}
