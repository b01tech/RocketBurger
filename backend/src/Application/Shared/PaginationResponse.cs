namespace Application.Shared;

public record PaginationResponse<T>(Pagination Pagination, IEnumerable<T> Items);

public record Pagination(int Page, int PageSize, int TotalItems)
{
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
}
