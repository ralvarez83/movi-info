import { Criteria } from "../../../../Shared/Domain/Criteria/Criteria";
import { Pagination } from "../../../../Shared/Domain/Criteria/Pagination";
import { FILTER_CONCATENATION, FIXED_FILTER_ADULT, FIXED_FILTER_LANGUAJE } from "./const.d";

const FILTER_DOMAIN_QUERY = 'byText'
export class TheMovieDBCriteriaTransformation {

  constructor(private readonly criteria: Criteria) {
  }

  public getCriterias(): string {
    const paginationFilter = '&page='

    let filters = FIXED_FILTER_ADULT + FILTER_CONCATENATION + FIXED_FILTER_LANGUAJE

    this.criteria.filters.filtersFiled.map(filter => {
      let filterField = (filter.field === FILTER_DOMAIN_QUERY) ? 'query': filter.field
      
      filters = filters + FILTER_CONCATENATION  + filterField + '=' + filter.value
    })

    return filters + paginationFilter + this.criteria.pagination.page
  }

  /**
   * isSearch
   */
  public isSearch() {
    return this.criteria.filters.filtersFiled.find(filter => {
      return (filter.field === FILTER_DOMAIN_QUERY && filter.value !== '')
    }) !== undefined
  }

  /**
   * getPagination
   */
  public getPagination() : Pagination {
    return this.criteria.pagination
  }
}