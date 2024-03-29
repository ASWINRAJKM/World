﻿using Microsoft.EntityFrameworkCore;
using WorldAPI.Data;
using WorldAPI.Models;
using WorldAPI.Repository.IRepository;

namespace WorldAPI.Repository
{
    public class StatesRepository : GenericRepository<States>,IStatesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StatesRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
       
        public async Task Update(States entity)
        {
            _dbContext.States.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
