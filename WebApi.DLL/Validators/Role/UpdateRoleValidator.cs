using FluentValidation;
using WebApi.BLL.Dtos.Role;

namespace WebApi.BLL.Validators.Role
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleDto>
    {
        public UpdateRoleValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Поле 'Id' не може бути пустим");
        }
    }
}
