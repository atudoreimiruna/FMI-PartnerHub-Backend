namespace Licenta.Services.QueryParameters;

public abstract class PaginationParameters
{
    readonly int maxPageSize = 20;
    private int _pageSize = 0;
    public int PageNumber { get; set; } = 1;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = value > maxPageSize ? maxPageSize : value;
        }
    }
}
