using System.Collections.Immutable;
using Shared.Domain.Criteria;
using MovieSearchResultsDomain = Movies.Domain.MovieSearchResults;

namespace WebAPI.dto
{
  public record MovieSearchResults
  {
    public ImmutableList<Movie> movies {get; set;}
    public Pagination pagination {get; set;}

    public static MovieSearchResults TranformToMovieSearchResults(MovieSearchResultsDomain movieSearchResultsDomain){
      return new MovieSearchResults{
        movies = movieSearchResultsDomain.movies.Select(movie => {
          return Movie.TransformToMovieDTO(movie);
        }).ToImmutableList<Movie>(),
        pagination = movieSearchResultsDomain.pagination
      };
    }
  }
}