using Licenta.Core.Extensions.PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Core.Interfaces;

public interface IRepository<T> where T : class
{
    IQueryable<T> AsQueryable();
    Task<T?> FindByIdAsync(params object[] pkValues);
    Task<PagedList<T>> FindAllAsync();
    Task<PagedList<T>> FindBySpecAsync(ISpecification<T>? spec);

    Task AddAsync(T entity);
    Task AddRangeAsync(ICollection<T> entities);

    Task UpdateAsync(T entity);
    Task UpdateRangeAsync(ICollection<T> entities);

    Task RemoveAsync(T entity);
    Task RemoveRangeAsync(ICollection<T> entities);
}