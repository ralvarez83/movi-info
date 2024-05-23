import { Criteria } from "../../../../Shared/Domain/Criteria/Criteria";
import { Pagination } from "../../../../Shared/Domain/Criteria/Pagination";

const FILTER_DOMAIN_FILTER_TEXT = 'byText'
export const FILTER_CONCATENATION = '&'

export class DotNetBackCriteriaTransformation {

  constructor(private readonly criteria: Criteria) {
  }

  public getCriterias(): string {
    const paginationFilter = 'page='
    const totalpagesFilter = 'totalpages='

    let filters : String = ""

    this.criteria.filters.filtersFiled.map(filter => {
      let filterField = (filter.field === FILTER_DOMAIN_FILTER_TEXT) ? FILTER_DOMAIN_FILTER_TEXT: filter.field
      
      filters += filterField + '=' + filter.value + FILTER_CONCATENATION
    })

    return filters + paginationFilter + this.criteria.pagination.page + FILTER_CONCATENATION + totalpagesFilter + (this.criteria.pagination.totalPage ? this.criteria.pagination.totalPage : 0)
  }

  /**
   * isSearch
   */
  public isSearch() {
    return this.criteria.filters.filtersFiled.find(filter => {
      return (filter.field === FILTER_DOMAIN_FILTER_TEXT && filter.value !== '')
    }) !== undefined
  }

  /**
   * getPagination
   */
  public getPagination() : Pagination {
    return this.criteria.pagination
  }
}