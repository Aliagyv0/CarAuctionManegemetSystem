using CarAuctionApi.Service.DTOs.Request;
using CarAuctionApi.Service.Helpers;
using FluentValidation;

namespace CarAuctionApi.Service.Validators;

public class NewsImageDtoValidator : AbstractValidator<NewsImageDto>
{
    public NewsImageDtoValidator()
    {
        RuleFor(p => p.Image).Custom((x, c) =>
        {
            if (x is not null)
            {
                if (!FileHelper.IsCorrectFileType(x, "image"))
                {
                    c.AddFailure("image", "File type is not correct.");
                }
            }
        });
    }   
}