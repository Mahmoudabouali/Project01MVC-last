using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Intrerfaces
{
    public interface IGenaricRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity?> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        void Update(TEntity entity);
    }
}
