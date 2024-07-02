using Movies.Application.Dtos;
using Movies.Application.Dtos.Transforms;
using Movies.Infraestructure.TheMovieDb;
using Shared.Domain.Bus.Query;

namespace Movies.Application.MovieSearch
{
    public class MovieSearchByCriteriaQueryHandler(MovieSearchByCriteria movieSearchByCriteria) : QueryHandler<MovieSearchByCriteriaQuery, MovieSearchResults>
    {
      private MovieSearchByCriteria _movieSearchByCriteria = movieSearchByCriteria;
        public async Task<MovieSearchResults> Handle(MovieSearchByCriteriaQuery query, CancellationToken cancellationToken)
        {
          MovieSearchResults movieSearchResults = TransformsToMovieSearchResultsDTO.Run(await _movieSearchByCriteria.Search(query.Criteria));
          return movieSearchResults;
        }
    }
}