using Acme.BookStore.Books;
using AutoMapper;
using BOOKSTore.Register;

namespace BOOKSTore;

public class BOOKSToreApplicationAutoMapperProfile : Profile
{
    public BOOKSToreApplicationAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        CreateMap<CustomerInfo, CustomerInfoDto>();
        CreateMap<CreateUpdateCustomerDto, CustomerInfo>();
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }
}
