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

            RuleFor(x => x.Image)
                .Custom((x, c) =>
                {
                    if (x != null)
                    {
                        if (!FileHelper.IsCorrectFileType(x, "image"))
                        {
                            c.AddFailure("Image", "Image is not a valid type.");
                        }

                        if (!FileHelper.IsSizeCorrect(x, 2))
                        {
                            c.AddFailure("Image", "Image is not a valid size.");
                        }
                    }
                });
        }
    }
}
