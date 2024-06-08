using Movies.Domain;

namespace Movies.Application
{
  public interface MovieSearch
  {
    public Task<MovieSearchResults> search();
  }
}