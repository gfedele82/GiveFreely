using Microsoft.Extensions.Logging;
using GiveFreely.Contracts.Engine;
using GiveFreely.DataAccess.Interfaces;
using GiveFreely.DataAccess.DTOAdapter;
using Newtonsoft.Json;

namespace GiveFreely.Engine
{
    public class CustomerEngine : ICustomerEngine
    {
        private readonly ICustomerRepository _repository;
        private readonly IAffiliateRepository _repositoryAffiliate;
        private readonly ILogger<CustomerEngine> _logger;

        public CustomerEngine(ICustomerRepository repository,
            IAffiliateRepository repositoryAffiliate,
           ILogger<CustomerEngine> logger)
        {
            _repository = repository;
            _repositoryAffiliate = repositoryAffiliate;
            _logger = logger;
        }

        public async Task<Models.Customers> GetById(int customerId)
        {
            try
            {
                _logger.LogInformation($"Customer Id: {customerId} to search");
                var entity = await _repository.GetByIdAsync(customerId);
                return entity.ToModel();
            }
            catch( Exception ex)
            {
                _logger.LogError($"Customer Id: {customerId} to search error: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<Models.Customers>> GetAll()
        {
            try
            {
                _logger.LogInformation($"Get All Customer");
                List<Models.Customers> listCustomer = new List<Models.Customers>();
                var entities = await _repository.GetAsync();
                Parallel.ForEach(entities, entity =>
                {
                    listCustomer.Add(entity.ToModel());
                });
                return listCustomer;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get All Customer error: {ex.Message}");
                return null;
            }
        }

        public async Task<Models.Customers> Add(Models.Customer customer)
        {
            try
            {
                _logger.LogInformation($"Customer to Add: {JsonConvert.SerializeObject(customer)}");
                var affiliate = await _repositoryAffiliate.GetByIdAsync(customer.IdAffiliate);
                if (affiliate == null) 
                {
                    _logger.LogError($"Add Customer Affiliate doesn't exist");
                    return new Models.Customers()
                    {
                        IdCustomer = 0
                    };
                }
                var entity = await _repository.SaveOrUptedeAsync(customer.ToDBModel());
                return entity.ToModel();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Add Customer error: {ex.Message}");
                return null;
            }
        }
    }
}
