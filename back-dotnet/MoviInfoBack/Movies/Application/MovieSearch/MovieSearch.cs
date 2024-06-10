using Movies.Application.DTO;

namespace Movies.Application.MovieSearch
{
  public interface MovieSearch
  {
    public Task<MovieSearchResults> search();
  }
}