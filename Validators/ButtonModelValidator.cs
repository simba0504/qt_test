using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASender.Models;
using FluentValidation;

namespace WASender.Validators
{
    public class ButtonModelValidator : AbstractValidator<ButtonsModel>
    {
        public ButtonModelValidator()
        {
            RuleFor(x => x.phoneNumber).NotEmpty().When(x => x.buttonTypeEnum == enums.ButtonTypeEnum.PHONE_NUMBER);
            RuleFor(x => x.url).NotEmpty().When(x => x.buttonTypeEnum == enums.ButtonTypeEnum.URL);
            RuleFor(x => x.text).NotEmpty();
            RuleFor(x => x.buttonTypeEnum.ToString()).NotEmpty();
        }
    }
}
