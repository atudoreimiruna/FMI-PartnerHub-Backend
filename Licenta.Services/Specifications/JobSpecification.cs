﻿using Licenta.Core.Entities;
using Licenta.Services.QueryParameters;

namespace Licenta.Services.Specifications;

public class JobSpecification : Specification<Job>
{
    public JobSpecification(JobParameters parameters)
    {
        if (!string.IsNullOrEmpty(parameters.Title))
        {
            AddCriteria(x => x.Title.ToLower().Contains(parameters.Title.ToLower()));
        }

        if (parameters.Experience != null)
        {
            AddCriteria(x => x.Experience == parameters.Experience);
        }

        if (parameters.MinSalary != null && parameters.MaxSalary != null)
        {
            AddCriteria(x => x.MinSalary <= parameters.MaxSalary && x.MinSalary >= parameters.MinSalary && x.MaxSalary >= parameters.MinSalary && x.MaxSalary <= parameters.MaxSalary);
        }

        switch (parameters.OrderBy)
        {
            case "Title":
                AddOrderBy(x => x.Title);
                break;

            case "Address":
                AddOrderBy(x => x.Address);
                break;

            default:
                break;
        }

        switch (parameters.OrderByDescending)
        {
            case "Title":
                AddOrderByDescending(x => x.Title);
                break;

            case "Address":
                AddOrderByDescending(x => x.Address);
                break;

            default:
                break;
        }

        AddPagination(parameters.PageNumber, parameters.PageSize);
    }
}