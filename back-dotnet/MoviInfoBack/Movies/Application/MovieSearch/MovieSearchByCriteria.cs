using Movies.Application.Dtos.Transforms;
using Movies.Domain;
using Shared.Domain.Criteria;

namespace Movies.Application.MovieSearch
{
  public class MovieSearchByCriteria : MovieSearch
  {
    private MovieRepository _repository;
    private Criteria _criteria;

    public MovieSearchByCriteria(MovieRepository repository, Criteria criteria){
      _criteria = criteria;
      _repository = repository;
    }

    public async Task<Dtos.MovieSearchResults> Search()
    {
        return TransformsToMovieSearchResultsDTO.Run(await _repository.searchByCriteria(_criteria));
    }
  }
}