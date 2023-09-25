using Licenta.Core.Entities;
using Licenta.Services.QueryParameters;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Licenta.Services.Specifications;

public class UserSpecification : Specification<User>
{
    public UserSpecification(UserParameters parameters)
    {
        if (!string.IsNullOrEmpty(parameters.UserName))
        {
            AddCriteria(x => x.UserName.ToLower().Contains(parameters.UserName.ToLower()));
        }

        switch (parameters.OrderBy)
        {
            case "UserName":
                AddOrderBy(x => x.UserName);
                break;

            default:
                break;
        }

        switch (parameters.OrderByDescending)
        {
            case "UserName":
                AddOrderByDescending(x => x.UserName);
                break;

            default:
                break;
        }

        AddInclude(x => x
           .Include(x => x.UserRoles)
           .ThenInclude(x => x.Role));

        AddPagination(parameters.PageNumber, parameters.PageSize);
    }
}