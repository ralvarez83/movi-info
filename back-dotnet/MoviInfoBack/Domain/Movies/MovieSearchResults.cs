using Domain.Shared.Criteria;

namespace Domain.Movies
{
  public class MovieSearchResults
  {
    public readonly Movie[] movies;
    public readonly Pagination pagination;
  }
}