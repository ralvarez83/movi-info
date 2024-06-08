using Shared.Domain.Criteria;
using Shared.Domain.Criteria.Filters;

namespace Movies.Infraestructure.TheMovieDb
{
  public class TheMovieDBCriteriaTransformation
  {
    private const string FIXED_FILTER_ADULT = "include_adult=false";
    private const string FIXED_FILTER_LANGUAJE = "language=es-ES";
    private const string FILTER_CONCATENATION = "&";
    private const string FILTER_STARTED = "?";
    private const string FILTER_ASIGNATION_VALUE = "=";
    
    private Criteria? _criteria;
    public TheMovieDBCriteriaTransformation(Criteria criteria)
    {
      _criteria = criteria;
    }

    public TheMovieDBCriteriaTransformation()
    {
    }

    public string getCriterias(){
      string filters = this.getEmptyCriteria();
      string paginationFilter = "&page=";

      if (null == _criteria)
        return filters;

      _criteria.filters.filtersFiled.ForEach(filter => {
        string filterField = (filter.field == Filter.FILTER_BY_TEXT)? "query": filter.field;

        filters += FILTER_CONCATENATION + filterField + FILTER_ASIGNATION_VALUE + filter.value;
      });

      return filters + paginationFilter + _criteria.pagination.page;
    }

    public bool isSearch() {
      return _criteria?.filters.filtersFiled.Find(filter => {
        return (filter.field == Filter.FILTER_BY_TEXT && !string.IsNullOrEmpty(filter.value));
      }) != null;
    }

    public Pagination? getPagination() {
      return _criteria?.pagination;
    }

    public string getEmptyCriteria (){
      return FILTER_STARTED + FIXED_FILTER_ADULT + FILTER_CONCATENATION + FIXED_FILTER_LANGUAJE;      
    }
  }
}