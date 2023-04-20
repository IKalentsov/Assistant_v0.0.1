using FluentValidation;

namespace Assistant.Application.Auth.Commands.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator() 
        {
            RuleFor(loginQuery => loginQuery.Email).NotEmpty().WithMessage((v) => "Email не должен быть пустой");
            RuleFor(loginQuery => loginQuery.Password).NotEmpty().WithMessage((v) => "Пароль не должен быть пустой");
            RuleFor(loginQuery => loginQuery.FirstName).NotEmpty().WithMessage((v) => "Поле имя не должно быть пустsм");
            RuleFor(loginQuery => loginQuery.LastName).NotEmpty().WithMessage((v) => "Поле фамилия не должно быть пустsм");
            RuleFor(loginQuery => loginQuery.Login).NotEmpty().WithMessage((v) => "Поле логин не должно быть пустsм");
        }
    }
}
