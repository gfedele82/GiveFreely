using FluentValidation.Results;
using FluentValidation;
using GiveFreely.Common;
using GiveFreely.Models;

namespace GiveFreely.Api.Validator
{
    public class AffiliateValidation : AbstractValidator<Affiliate>
    {
        public AffiliateValidation()
        {
            RuleFor(x => x.Name).Must(y => !string.IsNullOrEmpty(y)).WithMessage(ExcepcionsMenssages.NameNotNull);
        }

        protected override bool PreValidate(ValidationContext<Affiliate> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure("", ExcepcionsMenssages.AffiliateRequired));
                return false;
            }
            return true;
        }
    }
}
