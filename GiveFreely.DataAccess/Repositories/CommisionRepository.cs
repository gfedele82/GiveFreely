using GiveFreely.DataAccess.Interfaces;
using GiveFreely.DataAccess.Schema;
using Microsoft.EntityFrameworkCore;


namespace GiveFreely.DataAccess.Repositories
{
    public class CommisionRepository : ICommisionRepository
    {
        private readonly GFContext _dbContext;

        public CommisionRepository(GFContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Commision>> GetAsync()
        {
            return await _dbContext.Commisions.AsNoTracking().ToListAsync();
        }
    }
}
