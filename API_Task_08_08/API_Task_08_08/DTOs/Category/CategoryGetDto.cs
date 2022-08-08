using API_Task_08_08.DTOs.Book;
using System.Collections.Generic;

namespace API_Task_08_08.DTOs.Category
{
    public class CategoryGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookItemDto> Books { get; set; }
    }
}
