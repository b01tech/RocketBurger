export interface ApiPaginatedResponse<T> {
  items: T[];
  pagination: Pagination;
}

interface Pagination {
  page: number;
  pageSize: number;
  totalPages: number;
  totalItems: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}
