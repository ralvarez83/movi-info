using System.Collections.Immutable;
using movieSearchResultsDomain = Movies.Domain.MovieSearchResults;

namespace Movies.Application.DTO.Transforms
{
  public class TransformsToMovieSearchResultsDTO
  {
    public static MovieSearchResults Run (movieSearchResultsDomain movieSearchResultsToTranform){
      return new MovieSearchResults(
        movieSearchResultsToTranform.movies.Select(movie => {
          return TransformsToMovieDTO.Run(movie);
        }).ToImmutableList(),
        movieSearchResultsToTranform.pagination
      );
    }
    
  } 
}