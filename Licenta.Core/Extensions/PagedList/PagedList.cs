using System;
using System.Collections.Generic;

namespace Licenta.Core.Extensions.PagedList;

public class PagedList<T> : List<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool IsEnabled { get; set; }

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PagedList(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize, bool isEnabled)
    {
        TotalCount = totalCount;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        IsEnabled = isEnabled;
        AddRange(items);
    }
}