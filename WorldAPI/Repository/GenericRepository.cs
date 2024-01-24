using System.Linq.Expressions;
using WorldAPI.Data;
using WorldAPI.Repository.IRepository;

namespace WorldAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly ApplicationDbContext _dbContext;
        public GenericRepository(ApplicationDbContext dbContext)
        { 
            _dbContext = dbContext;
        }
        public async Task Create(T entity)
        {
            await _dbContext.AddAsync(entity);
            await Save();
        }

        public Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool IsRecordExists(Expression<Func<T, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
