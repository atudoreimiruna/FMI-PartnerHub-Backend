using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Licenta.Core.Interfaces;

public interface ISpecification<T>
{
    List<Expression<Func<T, bool>>> Criteria { get; }
    List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; }
    List<Expression<Func<T, object>>> OrderBy { get; }
    List<Expression<Func<T, object>>> OrderByDescending { get; }
    int PageSize { get; }
    int PageNumber { get; }
}
