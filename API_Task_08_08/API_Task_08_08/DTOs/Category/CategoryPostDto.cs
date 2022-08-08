using FluentValidation;

namespace API_Task_08_08.DTOs.Category
{
    public class CategoryPostDto
    {
        public string Name { get; set; }
    }
    public class CategoryPostDtoValidation:AbstractValidator<CategoryPostDto>
    {
        public CategoryPostDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("please enter value");
        }
    }
}
