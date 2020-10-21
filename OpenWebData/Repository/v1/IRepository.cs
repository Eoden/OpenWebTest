using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenWebData.Repository.v1
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        Task<List<TEntity>> GetAll();

        Task<TEntity> Add(TEntity entity);

        Task<TEntity> Update(TEntity entity);
        
    }
}
