namespace API_Task_08_08.DTOs.Book
{
    public class BookGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public short Pages { get; set; }
        public int CategoryId { get; set; }
        public CategoryInBookDto Category { get; set; }
    }
    public class CategoryInBookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BooksCount { get; set; }
    }
}
