using GiveFreely.DataAccess.Schema;

namespace GiveFreely.DataAccess.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAsync();
        Task<Customer> GetByIdAsync(int id);
        Task<Customer> SaveOrUptedeAsync(Customer customer);
    }
}
