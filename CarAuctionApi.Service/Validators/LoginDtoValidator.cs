using CarAuctionApi.Service.DTOs.Request.Identity;
using FluentValidation;

namespace CarAuctionApi.Service.Validators;

public class LoginDtoValidator:AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.UsernameOrEmail)
            .NotEmpty().NotNull().MaximumLength(100);
        RuleFor(x => x.Password)
            .NotEmpty().NotNull().MinimumLength(8);
    }   
}