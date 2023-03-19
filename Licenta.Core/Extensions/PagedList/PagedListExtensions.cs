using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Core.Extensions.PagedList;

public static class PagedListExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageNumber = 1, int pageSize = 0)
    {
        int count = source.Count();
        var items = pageSize == 0 ? source.ToList() : source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        pageSize = pageSize == 0 ? count : pageSize;
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
