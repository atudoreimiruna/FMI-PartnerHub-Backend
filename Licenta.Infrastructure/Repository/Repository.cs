using Licenta.Core.Extensions;
using Licenta.Core.Extensions.PagedList;
using Licenta.Core.Interfaces;

namespace Licenta.Infrastructure.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> AsQueryable()
    {
        return _context.Set<T>().AsQueryable();
    }

    public async Task<T?> FindByIdAsync(params object[] pkValues)
    {
        return await _context.Set<T>().FindAsync(pkValues);
    }

    public async Task<PagedList<T>> FindAllAsync()
    {
        return await _context.Set<T>().ToPagedListAsync();
    }

    public async Task<PagedList<T>> FindBySpecAsync(ISpecification<T>? spec = null)
    {
        return await SpecificationExtensions<T>.GetResult(_context.Set<T>().AsQueryable(), spec);
    }

    public async Task AddAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(ICollection<T> entities)
    {
        _context.Set<T>().AddRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(ICollection<T> entities)
    {
        _context.Set<T>().UpdateRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveRangeAsync(ICollection<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
        await _context.SaveChangesAsync();
    }
}