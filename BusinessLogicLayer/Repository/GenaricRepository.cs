using Demo.DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Repository
{
    public class GenaricRepository<TEntity> : IGenaricRepository<TEntity> where TEntity : class
    {
        private DataContext _dbContext;
        protected DbSet<TEntity> _entities;

        public GenaricRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity) => await _entities.AddAsync(entity);

        public void Delete(TEntity entity) => _entities.Remove(entity);

        public async Task<TEntity?> GetAsync(int id) => await _entities.FindAsync(id);
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _entities.ToListAsync();
        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);
    }
}
