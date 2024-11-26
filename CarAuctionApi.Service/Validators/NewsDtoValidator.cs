using CarAuctionApi.Service.DTOs.Request;
using CarAuctionApi.Service.Helpers;
using FluentValidation;

namespace CarAuctionApi.Service.Validators;

public class NewsDtoValidator: AbstractValidator<NewsDto>
{
    public NewsDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().NotNull().WithMessage("Title is required");
        RuleFor(x => x.Text).NotEmpty().NotNull().WithMessage("Text is required");
        RuleFor(x => x.Thesis).NotEmpty().NotNull().WithMessage("Thesis is required");
        RuleFor(p=>p.CategoryId).NotNull().NotEmpty();

    }   
}