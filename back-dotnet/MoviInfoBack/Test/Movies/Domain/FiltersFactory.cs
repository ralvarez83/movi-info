using Domain.Shared.Criteria.Filters;

namespace Test.Movies.Domain
{
  public class FiltersFactory
  {
    public static Filters BuildSpecificTextFilter(string value){
      Filters filters = new();

      filters.add(new Filter(Filter.FILTER_BY_TEXT, value, FilterOperator.EQUAL));

      return filters;
    }
  }
}