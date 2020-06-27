using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using RESTfullDemo.GraphQLSchema;
using System.Web.Http.Dependencies;

namespace RESTfullDemo.Extentions
{
    public static class GraphQLExtensions
    {
        public static void AddGraphQLSchemaAndTypes(this IServiceCollection services)
        {
            services.AddSingleton<AuthorType>();
            services.AddSingleton<BookType>();
            services.AddSingleton<LibraryQuery>();
            services.AddSingleton<ISchema, LibrarySchema>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            //services.AddSingleton<IDependencyResolver>(provider => new
            //    FuncDependencyResolver(
            //        type => provider.GetRequiredService(type)));
        }
    }
}
