using FluentValidation;
using WebApi.BLL.Dtos.Product;

namespace WebApi.BLL.Validators.Product
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Поле 'Id' не може бути пустим");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Поле 'Name' не може бути пустим");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Вартість не може бути від'ємною")
                .When(x => x.Price != default(decimal));

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Кількість не може бути від'ємною")
                .When(x => x.Amount != default(int));
        }
    }
}
