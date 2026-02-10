namespace Application.Shared;

public record PaginationResponse<T>(Pagination Pagination, IEnumerable<T> Items)
{
    public int TotalPages => (int)Math.Ceiling((double)Pagination.TotalItems / Pagination.PageSize);
}

public record Pagination(int Page, int PageSize, int TotalItems);
