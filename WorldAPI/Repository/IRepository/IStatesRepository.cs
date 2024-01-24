using WorldAPI.Models;

namespace WorldAPI.Repository.IRepository
{
    public interface IStatesRepository
    {
        Task<List<States>> GetAll();
        Task<States> GetById(int id);
        Task Create(States entity);
        Task Update(States entity);
        Task Delete(States entity);
        Task Save();
        bool IsStateExists(string name);

    }
}
