using FluentValidation;
using WebApi.BLL.Dtos.Category;

namespace WebApi.BLL.Validators.Category
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Поле 'Id' не може бути пустим");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Поле 'Name' не може бути пустим");
        }
    }
}
