using GiveFreely.DataAccess.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiveFreely.DataAccess.Interfaces
{
    public interface IAffiliateRepository
    {
        Task<IEnumerable<Affiliate>> GetAsync();
        Task<Affiliate> GetByIdAsync(int id);
        Task<Affiliate> SaveOrUptedeAsync(Affiliate affiliate);
    }
}
