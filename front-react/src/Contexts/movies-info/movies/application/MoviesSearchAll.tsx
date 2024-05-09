import { Criteria } from "../../../Shared/Domain/Criteria/Criteria";
import { Filters } from "../../../Shared/Domain/Criteria/Filters/Filters";
import { Order } from "../../../Shared/Domain/Criteria/Order/Order";
import { OrderType } from "../../../Shared/Domain/Criteria/Order/OrderTypes.d";
import { Pagination } from "../../../Shared/Domain/Criteria/Pagination";
import { MovieSearchResults } from "../domain/Movie";
import { MovieRepository } from "../domain/MovieRepository";
import { MoviesSearch } from "./types.d";

export class MoviesSearchAll implements MoviesSearch {
  constructor(private readonly repository: MovieRepository) {}

  async search () : Promise<MovieSearchResults> {
    const order: Order = new Order("", OrderType.NONE)
    const pagination: Pagination = new Pagination(1)
    const filters: Filters = new Filters()
    const criteria: Criteria = new Criteria(filters, order, pagination)

    return await this.repository.searchByCriteria(criteria);
  }

}