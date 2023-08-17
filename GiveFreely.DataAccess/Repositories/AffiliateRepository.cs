using GiveFreely.DataAccess.Interfaces;
using GiveFreely.DataAccess.Schema;
using Microsoft.EntityFrameworkCore;

namespace GiveFreely.DataAccess.Repositories
{
    public class AffiliateRepository : IAffiliateRepository
    {
        private readonly GFContext _dbContext;

        public AffiliateRepository(GFContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Affiliate>> GetAsync()
        {
            return await _dbContext.Affiliates.AsNoTracking().Include(k => k.Customers).ToListAsync();
        }

        public async Task<Affiliate> GetByIdAsync(int id)
        {
            return await _dbContext.Affiliates.AsNoTracking().Where(p => p.IdAffiliate == id).Include(k => k.Customers).FirstOrDefaultAsync();
        }

        public async Task<Affiliate> SaveOrUptedeAsync(Affiliate affiliate)
        {
            var entity = await _dbContext.Affiliates.FindAsync(affiliate.IdAffiliate);
            _dbContext.ChangeTracker.Clear();
            if (entity == null)
            {
                await _dbContext.Affiliates.AddAsync(affiliate);
            }
            else
            {
                _dbContext.Affiliates.Update(affiliate);
            }

            _dbContext.SaveChanges();
            return affiliate;


        }
    }
}
