import { Criteria } from "../../../../Shared/Domain/Criteria/Criteria";
import { Filters } from "../../../../Shared/Domain/Criteria/Filters/Filters";
import { Order } from "../../../../Shared/Domain/Criteria/Order/Order";
import { Pagination } from "../../../../Shared/Domain/Criteria/Pagination";

export class TheMovieDBCriteria extends Criteria {
  constructor(filters: Filters, order: Order, pagination: Pagination) {
    super(filters, order, pagination)
  }
}