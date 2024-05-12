using Domain.Movies;

namespace Application.Movies
{
  public interface MovieSearch
  {
    public Task<MovieSearchResults> search();
  }
}