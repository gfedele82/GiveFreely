using FluentValidation;
using GiveFreely.Api.Validator;
using GiveFreely.Common;
using GiveFreely.Models;
using System.Linq;
using Xunit;

namespace GiveFreely.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTestValidation
    {
        private IValidator<Models.Affiliate> _affiliateValidator;
        private IValidator<Models.Customer> _CustomerValidator;

        public UnitTestValidation()
        {
            _CustomerValidator = new CustomerValidation();
            _affiliateValidator = new AffiliateValidation();
        }

       [Fact]
        public  void CustomerValidation_OK()
        {
            var customer = new Customer()
            {
                Name = "Gustavo",
                IdAffiliate = 1
            };

            var result = _CustomerValidator.Validate(customer);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void CustomerValidation_Not_OK_Requiered_Affiliate()
        {
            var customer = new Customer()
            {
                Name = "Gustavo"
            };

            var result = _CustomerValidator.Validate(customer);

            Assert.False(result.IsValid);
            Assert.Equal(result.Errors.FirstOrDefault().ToString(), ExcepcionsMenssages.AffiliateRequired);
        }

        [Fact]
        public void CustomerValidation_Not_OK_Requiered_Name()
        {
            var customer = new Customer()
            {
                IdAffiliate = 1
            };

            var result = _CustomerValidator.Validate(customer);

            Assert.False(result.IsValid);
            Assert.Equal(result.Errors.FirstOrDefault().ToString(), ExcepcionsMenssages.NameNotNull);
        }

        [Fact]
        public void AffiliateValidation_Not_OK()
        {
            var affiliate = new Affiliate()
            {
                Name = "Mike",
            };

            var result = _affiliateValidator.Validate(affiliate);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void AffiliateValidation_Not_OK_Requiered_Name()
        {
            var affiliate = new Affiliate()
            {
                Name = "",
            };

            var result = _affiliateValidator.Validate(affiliate);

            Assert.False(result.IsValid);
            Assert.Equal(result.Errors.FirstOrDefault().ToString(), ExcepcionsMenssages.NameNotNull);
        }
    }
}
