export class Pagination {
  offset: number | undefined
  limit: number |  undefined

  constructor(offset?: number, limit?: number) {
    this.offset = offset
    this.limit = limit
  }
}