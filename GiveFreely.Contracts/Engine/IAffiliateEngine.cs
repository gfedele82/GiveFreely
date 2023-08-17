using GiveFreely.Models;

namespace GiveFreely.Contracts.Engine
{
    public interface IAffiliateEngine
    {
        Task<Affiliates> GetById(int affilieateId);

        Task<IEnumerable<Affiliates>> GetAll();

        Task<Affiliates> Add(Affiliate affiliate);
    }
}
