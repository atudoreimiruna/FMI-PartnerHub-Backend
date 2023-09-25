namespace Licenta.Services.QueryParameters;

public class BaseParameters : PaginationParameters
{
    public string OrderBy { get; set; }
    public string OrderByDescending { get; set; }
}

