using GraphQL.Types;
using RESTfullDemo.Entities;
using RESTfullDemo.Services;

namespace RESTfullDemo.GraphQLSchema
{
    public class AuthorType : ObjectGraphType<Author>
    {
        public AuthorType(IRepositoryWrapper repositoryWrapper)
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);
            Field(x => x.BirthDate);
            Field(x => x.BirthPlace);
            Field(x => x.Name);
            Field<ListGraphType<BookType>>("books", resolve: context=>
            {
                return repositoryWrapper.Book.GetBooksAsync(context.Source.Id).Result;
            });
        }
    }
}
