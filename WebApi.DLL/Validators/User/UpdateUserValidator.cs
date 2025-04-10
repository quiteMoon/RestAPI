using FluentValidation;
using WebApi.BLL.Dtos.AppUser;

namespace WebApi.BLL.Validators.User
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Поле 'Id' не може бути пустим");

            RuleFor(x => x.UserName)
                .MinimumLength(4).WithMessage("Ім'я повинне містити мінімум 4 символи")
                .When(x => x.UserName != default(string));

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Невірний формат пошти")
                .When(x => x.Email != default(string)); ;
        }
    }
}
