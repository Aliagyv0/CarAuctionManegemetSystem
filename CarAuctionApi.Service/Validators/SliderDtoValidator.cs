using CarAuctionApi.Service.DTOs.Request;
using CarAuctionApi.Service.Helpers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Service.Validators
{
    public class SliderDtoValidator : AbstractValidator<SliderDto>
    {
        public SliderDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().NotNull().MaximumLength(40);

            RuleFor(x => x.Description)
                .NotEmpty().NotNull().MaximumLength(200);

            RuleFor(x => x)
                .Custom((x, c) =>
                {
                    if (x.Image != null)
                    {
                        if (!FileHelper.IsCorrectFileType(x.Image, "image"))
                        {
                            c.AddFailure("Image", "Image is not a valid type.");
                        }

                        if (!FileHelper.IsSizeCorrect(x.Image, 2))
                        {
                            c.AddFailure("Image", "Image is not a valid size.");
                        }
                    }
                });
        }
    }
}
