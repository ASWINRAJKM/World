using WorldAPI.Models;

namespace WorldAPI.Repository.IRepository
{
    public interface IStatesRepository :IGenericRepository<States>
    {
        Task Update(States entity);
 
    }
}
