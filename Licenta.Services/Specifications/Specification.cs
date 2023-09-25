using Microsoft.EntityFrameworkCore.Query;
using Licenta.Core.Interfaces;
using Licenta.Services.QueryParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Licenta.Services.Specifications;

public class Specification<T> : PaginationParameters, ISpecification<T>
{
    public List<Expression<Func<T, bool>>> Criteria { get; private set; } = new List<Expression<Func<T, bool>>>();
    public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; private set; } = new List<Func<IQueryable<T>, IIncludableQueryable<T, object>>>();
    public List<Expression<Func<T, object>>> OrderBy { get; private set; } = new List<Expression<Func<T, object>>>();
    public List<Expression<Func<T, object>>> OrderByDescending { get; private set; } = new List<Expression<Func<T, object>>>();

    public Specification() { }
    public Specification(Expression<Func<T, bool>> criteriaExpr)
    {
        Criteria.Add(criteriaExpr);
    }

    protected void AddCriteria(Expression<Func<T, bool>> criteriaExpr)
    {
        Criteria.Add(criteriaExpr);
    }

    protected void AddInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> includeFunc)
    {
        Includes.Add(includeFunc);
    }

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpr)
    {
        OrderBy.Add(orderByExpr);
    }

    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpr)
    {
        OrderByDescending.Add(orderByDescExpr);
    }

    protected void AddPagination(int? pageNumber = null, int? pageSize = null)
    {
        PageNumber = pageNumber ?? 1;
        PageSize = pageSize ?? 0;
    }
}