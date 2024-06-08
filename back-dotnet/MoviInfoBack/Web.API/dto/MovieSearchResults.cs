using System.Collections.Immutable;
using Domain.Shared.Criteria;
using MovieSearchResultsDomain = Domain.Movies.MovieSearchResults;

namespace WebAPI.dto
{
  public record MovieSearchResults
  {
    public ImmutableList<Movie> movies {get; init;}
    public Pagination pagination {get; init;}

    public MovieSearchResults(MovieSearchResultsDomain movieSearchResultsDomain){

      this.movies = movieSearchResultsDomain.movies.Select(movie => {
        return Movie.TransformToMovieDTO(movie);
      }).ToImmutableList<Movie>();
      this.pagination = movieSearchResultsDomain.pagination;
    }
  }
}