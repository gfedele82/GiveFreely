using GiveFreely.Contracts.Engine;
using GiveFreely.DataAccess.DTOAdapter;
using GiveFreely.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GiveFreely.Engine
{
    public class AffiliateEngine : IAffiliateEngine
    {
        private readonly IAffiliateRepository _repository;
        private readonly ILogger<AffiliateEngine> _logger;

        public AffiliateEngine(IAffiliateRepository repository,
           ILogger<AffiliateEngine> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Models.Affiliates> GetById(int affiliateId)
        {
            try
            {
                _logger.LogInformation($"Affiliate Id: {affiliateId} to search");
                var entity = await _repository.GetByIdAsync(affiliateId);
                return entity.ToModel();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Affiliate Id: {affiliateId} to search error: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<Models.Affiliates>> GetAll()
        {
            try
            {
                _logger.LogInformation($"Get All Affiliate");
                List<Models.Affiliates> listAffiliate = new List<Models.Affiliates>();
                var entities = await _repository.GetAsync();
                Parallel.ForEach(entities, entity =>
                {
                    listAffiliate.Add(entity.ToModel());
                });
                return listAffiliate;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get All Affiliate error: {ex.Message}");
                return null;
            }
        }

        public async Task<Models.Affiliates> Add(Models.Affiliate affiliate)
        {
            try
            {
                _logger.LogInformation($"Affiliate to Add: {JsonConvert.SerializeObject(affiliate)}");
                var entity = await _repository.SaveOrUptedeAsync(affiliate.ToDBModel());
                return entity.ToModel();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Add Affiliate error: {ex.Message}");
                return null;
            }
        }
    }
}
