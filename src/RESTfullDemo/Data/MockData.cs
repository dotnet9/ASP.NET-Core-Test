using RESTfullDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfullDemo.Data
{
    public class MockData
    {
        public static MockData Current { get; } = new MockData();
        public List<AuthorDto> Authors { get; set; }
        public List<BookDto> Books { get; set; }

        public MockData()
        {
            Authors = new List<AuthorDto>();
            Books = new List<BookDto>();
            int authorCount = 10;
            Random rd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < authorCount; i++)
            {
                var author = new AuthorDto
                {
                    Id = Guid.NewGuid(),
                    Name = $"Author {i}",
                    Age = rd.Next(25, 100),
                    Email = $"author{i}@xxx.com"
                };
                Authors.Add(author);

                for (int j = 0; j < rd.Next(2, 10); j++)
                {
                    Books.Add(new BookDto
                    {
                        Id = Guid.NewGuid(),
                        Title = $"Book {j} {author.Name}",
                        Description = $"Description of Bokk {j} {author.Name}",
                        Pages = rd.Next(250, 1000),
                        AuthorId = author.Id
                    });
                }
            }

        }
    }
}
