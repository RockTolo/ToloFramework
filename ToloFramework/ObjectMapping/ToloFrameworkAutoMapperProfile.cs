using AutoMapper;
using ToloFramework.Entities.Books;
using ToloFramework.Services.Dtos.Books;

namespace ToloFramework.ObjectMapping;

public class ToloFrameworkAutoMapperProfile : Profile
{
    public ToloFrameworkAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        CreateMap<BookDto, CreateUpdateBookDto>();
        /* Create your AutoMapper object mappings here */
    }
}
