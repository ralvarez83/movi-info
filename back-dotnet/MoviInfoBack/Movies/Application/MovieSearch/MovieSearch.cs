using Movies.Domain;
using Shared.Domain.Criteria;

namespace Movies.Application.MovieSearch
{
  public interface MovieSearch
  {
    public Task<MovieSearchResults> Search(Criteria criteria);
  }
}