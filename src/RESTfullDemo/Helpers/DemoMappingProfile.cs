using AutoMapper;
using RESTfullDemo.Entities;
using RESTfullDemo.Models;

namespace RESTfullDemo.Helpers
{
    public class DemoMappingProfile : Profile
    {
        public DemoMappingProfile()
        {
            CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.Age, config =>
                  config.MapFrom(src => src.BirthDate));
            CreateMap<Book, BookDto>();
            CreateMap<BookForCreationDto, Book>();
            CreateMap<BookForUpdateDto, Book>();

        }
    }
}
