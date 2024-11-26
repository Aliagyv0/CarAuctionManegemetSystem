using CarAuctionApi.Service.DTOs.Request.Identity;
using FluentValidation;

namespace CarAuctionApi.Service.Validators;

public class RegisterDtoValidator: AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().NotNull().MaximumLength(40);
        RuleFor(x => x.Email)
            .NotEmpty().NotNull().MaximumLength(40);
        RuleFor(x => x.FinCode)
            .NotEmpty().NotNull().MinimumLength(7).MaximumLength(10);
        RuleFor(x => x.SerialNumber)
            .NotEmpty().NotNull().MaximumLength(15).MinimumLength(15);
        RuleFor(x => x.Email)
            .NotEmpty().NotNull().EmailAddress();
        RuleFor(x => x.UserName)
            .NotEmpty().NotNull().MaximumLength(40);
        RuleFor(x => x.Password)
            .NotEmpty().NotNull().MinimumLength(8);
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().NotNull().Equal(x => x.Password).MinimumLength(8);
    }
}

