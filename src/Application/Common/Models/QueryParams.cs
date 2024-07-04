using AssetManagement.Domain.Constants;

public abstract record QueryParams(
    string SortColumnName,
    int PageNumber = AppPagingConstants.DefaultPageNumber,
    int PageSize = AppPagingConstants.DefaultPageSize,
    string SortColumnDirection = AppPagingConstants.DefaultSortDirection
    );