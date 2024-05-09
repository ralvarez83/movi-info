export class Pagination {
  pageSize?: number

  constructor(readonly page: number, readonly totalPage?: number) {
  }

  public isLastPage() : boolean {
    return this.totalPage === this.page
  }

  /**
   * getNextPage
   */
  public getNextPage(): Pagination {
    return (this.totalPage === this.page)? this: new Pagination(this.page + 1, this.totalPage)
  }
}