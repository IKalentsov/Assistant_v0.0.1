using FluentValidation;

namespace Assistant.Application.Auth.Query.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator() 
        {
            RuleFor(loginQuery => loginQuery.Login).NotEmpty().WithMessage((v) => "Логин не должен быть пустой");

            RuleFor(loginQuery => loginQuery.Password).NotEmpty().WithMessage((v) => "Пароль не должен быть пустой");
        }
    }
}
