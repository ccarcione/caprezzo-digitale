export class PageInfo {
    totalCount: number;
    pageSize: number;
    currentPage: number;
    totalPages: number;

    hasPrevious: boolean;
    hasNext: boolean;
  }
  
  export class PaginationResult<T> {
    data: T[];
    pagination: PageInfo;
  }
  