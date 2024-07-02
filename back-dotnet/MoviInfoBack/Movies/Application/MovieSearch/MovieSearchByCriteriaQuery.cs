using Shared.Domain.Bus.Query;
using Shared.Domain.Criteria;

namespace Movies.Application.MovieSearch
{
  public class MovieSearchByCriteriaQuery (Criteria criteria): Query
  {
    public Criteria Criteria {get;} = criteria;
  }
}