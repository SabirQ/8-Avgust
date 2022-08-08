namespace API_Task_08_08.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public short Pages { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
