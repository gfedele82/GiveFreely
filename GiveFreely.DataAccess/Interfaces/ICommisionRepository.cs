using GiveFreely.DataAccess.Schema;

namespace GiveFreely.DataAccess.Interfaces
{
    public interface ICommisionRepository
    {
        Task<IEnumerable<Commision>> GetAsync();
    }
}
