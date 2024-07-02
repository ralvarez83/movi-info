using Movies.Application.Dtos;
using Shared.Domain.Criteria;

namespace Movies.Application.MovieSearch
{
  public interface MovieSearch
  {
    public Task<MovieSearchResults> Search(Criteria criteria);
  }
}