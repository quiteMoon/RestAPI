using FluentValidation;
using WebApi.BLL.Dtos.Category;

namespace WebApi.BLL.Validators.Category
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ім'я не може бути порожнім");
        }
    }
}
