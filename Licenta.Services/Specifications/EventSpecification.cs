using Licenta.Core.Entities;
using Licenta.Services.QueryParameters;
using Microsoft.EntityFrameworkCore;

namespace Licenta.Services.Specifications;

public class EventSpecification : Specification<Event>
{
    public EventSpecification(EventParameters parameters)
    {
        if (!string.IsNullOrEmpty(parameters.Title))
        {
            AddCriteria(x => x.Title.ToLower().Contains(parameters.Title.ToLower()));
        }

        if (!string.IsNullOrEmpty(parameters.Type))
        {
            AddCriteria(x => x.Type.ToLower().Contains(parameters.Type.ToLower()));
        }

        if (!string.IsNullOrEmpty(parameters.Location))
        {
            AddCriteria(x => x.Location.ToLower().Contains(parameters.Location.ToLower()));
        }

        if (!string.IsNullOrEmpty(parameters.PartnerName))
        {
            AddCriteria(x => x.Partner.Name.ToLower().Contains(parameters.PartnerName.ToLower()));
        }

        if (!string.IsNullOrEmpty(parameters.PartnerName))
        {
            AddCriteria(x => x.Partner.Id.Equals(parameters.PartnerId));
        }

        switch (parameters.OrderBy)
        {
            case "LastUpdated":
                AddOrderBy(x => x.LastUpdated);
                break;

            default:
                break;
        }

        switch (parameters.OrderByDescending)
        {
            case "LastUpdated":
                AddOrderByDescending(x => x.LastUpdated);
                break;

            default:
                break;
        }

        AddInclude(x => x
           .Include(x => x.Partner)
           .Include(x => x.Files));

        AddPagination(parameters.PageNumber, parameters.PageSize);
    }
}
