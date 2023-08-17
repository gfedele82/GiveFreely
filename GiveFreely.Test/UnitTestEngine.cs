using Xunit;
using GiveFreely.Models;
using GiveFreely.Contracts.Engine;
using GiveFreely.Engine;
using Microsoft.Extensions.Logging;
using GiveFreely.DataAccess.Interfaces;
using Moq;
using GiveFreely.DataAccess.DTOAdapter;

namespace GiveFreely.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTestEngine
    {
        private readonly Mock<ICustomerRepository> _repositoryCustomer;
        private readonly Mock<IAffiliateRepository> _repositoryAffiliate;
        private readonly Mock<ILogger<CustomerEngine>> _loggerCustomer;
        private readonly Mock<ILogger<AffiliateEngine>> _loggerAffiliate;
        private readonly ICustomerEngine _customerEngine;
        private readonly IAffiliateEngine _affiliateEngine;

        public UnitTestEngine()
        {
            _repositoryCustomer = new Mock<ICustomerRepository>();
            _repositoryAffiliate = new Mock<IAffiliateRepository>();

            _loggerCustomer = new Mock<ILogger<CustomerEngine>>();
            _loggerAffiliate = new Mock<ILogger<AffiliateEngine>>();

            _customerEngine = new CustomerEngine(_repositoryCustomer.Object, _repositoryAffiliate.Object, _loggerCustomer.Object);
            _affiliateEngine = new AffiliateEngine(_repositoryAffiliate.Object, _loggerAffiliate.Object);
        }

        [Fact]
        public async void CreateAffiliate_ReturnsCreated()
        {

            var affiliate = new Affiliate()
            {
                Name = "Mike",
            };

            var expectedVal = new Affiliate()
            {
                IdAffiliate= 1,
                Name = "Mike",
            };
            _repositoryAffiliate.Setup(p => p.SaveOrUptedeAsync(It.IsAny<DataAccess.Schema.Affiliate>()).Result).Returns(expectedVal.ToDBModel());

            var result = await _affiliateEngine.Add(affiliate);

            Assert.Equal(expectedVal.Name, result.Name);
        }

        [Fact]
        public async void CreateCustomer_ReturnsCreated()
        {
            var customer = new Customer()
            {
                IdAffiliate = 1,
                Name = "Gustavo"
            };

            var affiliate = new Affiliate()
            {
                IdAffiliate = 1,
                Name = "Mike"
            };

            var expectedVal = new Customer()
            {
                IdAffiliate = 1,
                Name = "Gustavo",
                IdCustomer = 1,
                
            };

            _repositoryAffiliate.Setup(p => p.GetByIdAsync(It.IsAny<int>()).Result).Returns(affiliate.ToDBModel());
            _repositoryCustomer.Setup(p => p.SaveOrUptedeAsync(It.IsAny<DataAccess.Schema.Customer>()).Result).Returns(expectedVal.ToDBModel());

            var result = await _customerEngine.Add(customer);

            Assert.Equal(expectedVal.Name, result.Name);
        }

        [Fact]
        public async void CreateCustomer_ReturnsNotCreated()
        {
            var customer = new Customer()
            {
                IdAffiliate = 1,
                Name = "Gustavo"
            };

            var expectedVal = new Customer()
            {
                IdCustomer = 0,

            };

            _repositoryAffiliate.Setup(p => p.GetByIdAsync(It.IsAny<int>()).Result).Returns(value: null);
            _repositoryCustomer.Setup(p => p.SaveOrUptedeAsync(It.IsAny<DataAccess.Schema.Customer>()).Result).Returns(expectedVal.ToDBModel());

            var result = await _customerEngine.Add(customer);

            Assert.Equal(expectedVal.IdCustomer, result.IdCustomer);
        }

    }
}
