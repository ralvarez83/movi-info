using Movies.Application.Dtos.Transforms;
using Movies.Domain;
using Shared.Domain.Criteria;

namespace Movies.Application.MovieSearch
{
  public class MovieSearchByCriteria (MovieRepository repository): MovieSearch
  {
    private MovieRepository _repository = repository;

    public async Task<Dtos.MovieSearchResults> Search(Criteria criteria)
    {
        return TransformsToMovieSearchResultsDTO.Run(await _repository.searchByCriteria(criteria));
    }
  }
}