using System;
using ToloFramework.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using ToloFramework.Services.Dtos.Books;
using ToloFramework.Entities.Books;

namespace ToloFramework.Services.Books;

public class BookAppService :
    CrudAppService<
        Book, //The Book entity
        BookDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateBookDto>, //Used to create/update a book
    IBookAppService //implement the IBookAppService
{
    public BookAppService(IRepository<Book, Guid> repository)
        : base(repository)
    {
        GetPolicyName = ToloFrameworkPermissions.Books.Default;
        GetListPolicyName = ToloFrameworkPermissions.Books.Default;
        CreatePolicyName = ToloFrameworkPermissions.Books.Create;
        UpdatePolicyName = ToloFrameworkPermissions.Books.Edit;
        DeletePolicyName = ToloFrameworkPermissions.Books.Delete;
    }
}