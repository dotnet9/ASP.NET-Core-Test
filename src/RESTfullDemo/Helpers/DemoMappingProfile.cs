using AutoMapper;
using RESTfullDemo.Entities;
using RESTfullDemo.Models;
using System;

namespace RESTfullDemo.Helpers
{
    public class DemoMappingProfile : Profile
    {
        public DemoMappingProfile()
        {
            CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.Age, config =>
                  config.MapFrom(src => (src.BirthDate.Year - DateTime.Now.Year)));
            CreateMap<AuthorForCreationDto, Author>();
            CreateMap<Author, AuthorDto>();
            CreateMap<Book, BookDto>();
            CreateMap<BookForCreationDto, Book>();
            CreateMap<BookForUpdateDto, Book>();

        }
    }
}
