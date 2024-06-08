using Movies.Domain.ValueObjects;
using Shared.Domain.Criteria;

namespace Movies.Domain
{
  public interface MovieRepository
  {
    public Task<MovieSearchResults> searchByCriteria (Criteria criteria);

    public Task<Movie?> findById(MovieId movieId);
  }
}