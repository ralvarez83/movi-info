using Movies.Domain;
using Shared.Domain.Criteria;

namespace Movies.Application.MovieSearch
{
  public class MovieSearchByCriteria (MovieRepository repository): MovieSearch
  {
    private MovieRepository _repository = repository;

    public async Task<MovieSearchResults> Search(Criteria criteria)
    {
        return await _repository.searchByCriteria(criteria);
    }
  }
}