using Movies.Application.Dtos;

namespace Movies.Application.MovieSearch
{
  public interface MovieSearch
  {
    public Task<MovieSearchResults> Search();
  }
}