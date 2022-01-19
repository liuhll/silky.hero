export interface BasicPageParams {
  PageIndex: number;
  pageSize: number;
}

export interface BasicFetchResult<T> {
  items: T[];
  totalCount: number;
  totalPages: number;
  hasPrevPages: boolean;
  hasNextPages: boolean;
}
