import { Criteria } from "../../../../Shared/Domain/Criteria/Criteria";
import { Pagination } from "../../../../Shared/Domain/Criteria/Pagination";

const FILTER_DOMAIN_QUERY = 'byText'
export class TheMovieDBCriteriaTransformation {

  constructor(private readonly criteria: Criteria) {
  }

  public getCriterias(): string {
    const fixedFilters = 'include_adult=false&language=es-ES'
    const paginationFilter = '&page='
    const filterConcatenation = '&'

    let filters = fixedFilters

    this.criteria.filters.filtersFiled.map(filter => {
      let filterField = (filter.field === FILTER_DOMAIN_QUERY) ? 'query': filter.field
      
      filters = filters + filterConcatenation  + filterField + '=' + filter.value
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