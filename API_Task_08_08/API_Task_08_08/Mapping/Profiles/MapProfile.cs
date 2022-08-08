using API_Task_08_08.DTOs.Book;
using API_Task_08_08.DTOs.Category;
using API_Task_08_08.Model;
using AutoMapper;

namespace API_Task_08_08.Mapping.Profiles
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Book, BookGetDto>();
            CreateMap<Book, BookItemDto>();
            CreateMap<BookPostDto, Book>();
            CreateMap<Category, CategoryGetDto>();
            CreateMap<Category, CategoryItemDto>();
            CreateMap<Category, CategoryInBookDto>();
            CreateMap<CategoryPostDto, Category>();
        }
    }
}
