using FluentValidation;
using WebApi.BLL.Dtos.Product;

namespace WebApi.BLL.Validators.Product
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ім'я не може бути порожнім");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Вкажіть ціну продукта")
                .GreaterThan(0).WithMessage("Вартість не може бути від'ємною");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Вкажіть кількість")
                .GreaterThan(0).WithMessage("Кількість не може бути від'ємною");
        }
    }
}
