using Domain.Movies.ValueObjects;
using Domain.Shared.Criteria;

namespace Domain.Movies
{
  public interface MovieRepository
  {
    public Task<MovieSearchResults> searchByCriteria (Criteria criteria);

    public Task<Movie?> findById(MovieId movieId);
  }
}