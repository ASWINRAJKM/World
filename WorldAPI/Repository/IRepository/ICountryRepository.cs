using WorldAPI.Models;

namespace WorldAPI.Repository.IRepository
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task Update(Country entity);

    }
}
