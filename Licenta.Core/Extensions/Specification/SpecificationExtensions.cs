using Licenta.Core.Extensions.PagedList;
using Licenta.Core.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Core.Extensions;

public class SpecificationExtensions<T>
{
    public static async Task<PagedList<T>> GetResult(IQueryable<T> inputQuery, ISpecification<T> spec)
    {
        var query = inputQuery;
        if (spec != null)
        {
            foreach (var criteria in spec.Criteria)
                query = query.Where(criteria);

            query = spec.Includes.Aggregate(query, (current, include) => include(current));

            return await query.ToPagedListAsync(spec.PageNumber, spec.PageSize);
        }
        return await query.ToPagedListAsync();
    }
}
