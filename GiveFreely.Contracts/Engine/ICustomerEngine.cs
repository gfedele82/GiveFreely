using GiveFreely.Models;

namespace GiveFreely.Contracts.Engine
{
    public interface ICustomerEngine
    {
        Task<Customers> GetById(int customerId);

        Task<IEnumerable<Customers>> GetAll();

        Task<Customers> Add(Customer customer);
    }
}
