using CarAuctionApi.Service.DTOs.Request;
using FluentValidation;

namespace CarAuctionApi.Service.Validators;

public class CategoryDtoValidator:AbstractValidator<CategoryDto>
{
    public CategoryDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().NotNull().MaximumLength(40);
    }
}