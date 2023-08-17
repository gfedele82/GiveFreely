using FluentValidation.Results;
using FluentValidation;
using GiveFreely.Common;
using GiveFreely.Models;

namespace GiveFreely.Api.Validator
{
    public class CustomerValidation : AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(x => x.Name).Must(y => !string.IsNullOrEmpty(y)).WithMessage(ExcepcionsMenssages.NameNotNull);
            RuleFor(x => x.IdAffiliate).Must(y => y > 0).WithMessage(ExcepcionsMenssages.AffiliateRequired);
        }

        protected override bool PreValidate(ValidationContext<Customer> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure("", ExcepcionsMenssages.CustomerRequired));
                return false;
            }
            return true;
        }
    }
}
