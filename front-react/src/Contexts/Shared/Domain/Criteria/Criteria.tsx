import { Filters } from "./Filters/Filters";
import { Order } from "./Order/Order";
import { Pagination } from "./Pagination";


export class Criteria {

  filters: Filters
  order: Order
  pagination: Pagination

  constructor(filters: Filters, order: Order, pagination: Pagination) {
    this.filters = filters
    this.order = order
    this.pagination = pagination
  }
}