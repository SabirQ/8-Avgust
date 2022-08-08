using FluentValidation;

namespace API_Task_08_08.DTOs.Book
{
    public class BookPostDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public short Pages { get; set; }
        public int CategoryId { get; set; }
    }
    public class BookPostDtoValidation : AbstractValidator<BookPostDto>
    {
        public BookPostDtoValidation()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("enter value")
                .MaximumLength(50).WithMessage("maximum length must be 50");
            RuleFor(c => c.Pages).NotEmpty().WithMessage("enter value")
                .GreaterThanOrEqualTo((short)5).WithMessage("min pages are 5")
                .LessThanOrEqualTo((short)10000).WithMessage("max pages 10000");
            RuleFor(c => c.CategoryId).NotEmpty().WithMessage("enter value");
            RuleFor(c => c.Price).NotEmpty().WithMessage("enter value")
                .GreaterThanOrEqualTo(1.00m).WithMessage("Minimum price is 1")
                .LessThanOrEqualTo(9999.99m).WithMessage("Max price is 9999.99");
        }
    }
}
