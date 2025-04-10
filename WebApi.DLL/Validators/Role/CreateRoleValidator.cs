using FluentValidation;
using WebApi.BLL.Dtos.Role;

namespace WebApi.BLL.Validators.Role
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleDto>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Поле 'Name' не може бути пустим");
        }
    }
}
