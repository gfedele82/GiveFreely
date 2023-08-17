using Microsoft.EntityFrameworkCore;
using GiveFreely.DataAccess.Interfaces;
using GiveFreely.DataAccess.Schema;

namespace GiveFreely.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly GFContext _dbContext;

        public CustomerRepository(GFContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Customer>> GetAsync()
        {
            return await _dbContext.Customers.AsNoTracking().Include(k => k.Affiliate).ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _dbContext.Customers.AsNoTracking().Where(p => p.IdCustomer == id).Include(k => k.Affiliate).FirstOrDefaultAsync();
        }

        public async Task<Customer> SaveOrUptedeAsync(Customer customer)
        {
            var entity = await _dbContext.Customers.FindAsync(customer.IdCustomer);
            _dbContext.ChangeTracker.Clear();
            if (entity == null)
            {
                await _dbContext.Customers.AddAsync(customer);
            }
            else 
            {
                _dbContext.Customers.Update(customer);
            }

            _dbContext.SaveChanges();
            return customer;


        }
    }
}
