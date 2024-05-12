using Domain.Movies;
using Domain.Shared.Criteria;

namespace Application.Movies
{
  public class MovieSearchByCriteria : MovieSearch
  {
    private MovieRepository _repository;
    private Criteria _criteria;

    public MovieSearchByCriteria(MovieRepository repository, Criteria criteria){
      _criteria = criteria;
      _repository = repository;
    }

    public async Task<MovieSearchResults> search()
    {
        return await _repository.searchByCriteria(_criteria);
    }
    }
}