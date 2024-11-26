using CarAuctionApi.Service.DTOs.Request;
using FluentValidation;

namespace CarAuctionApi.Service.Validators;

public class TagDtoValidator: AbstractValidator<TagDto>
{
    public TagDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().NotNull().MaximumLength(40);
    }
}