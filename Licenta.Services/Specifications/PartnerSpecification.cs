using Licenta.Core.Entities;
using Licenta.Services.QueryParameters.Partner;

namespace Licenta.Services.Specifications;

public class PartnerSpecification : Specification<Partner>
{
    public PartnerSpecification(PartnerParameters parameters)
    {
        if (!string.IsNullOrEmpty(parameters.Name))
        {
            AddCriteria(x => x.Name.ToLower().Contains(parameters.Name.ToLower()));
        }

        switch (parameters.OrderBy)
        {
            case "Name":
                AddOrderBy(x => x.Name);
                break;

            default:
                break;
        }

        switch (parameters.OrderByDescending)
        {
            case "Name":
                AddOrderByDescending(x => x.Name);
                break;

            default:
                break;
        }

        AddPagination(parameters.PageNumber, parameters.PageSize);
    }
}
