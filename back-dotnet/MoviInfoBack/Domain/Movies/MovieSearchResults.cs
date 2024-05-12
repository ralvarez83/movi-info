using System.Collections.Immutable;
using Domain.Shared.Criteria;

namespace Domain.Movies
{
  public sealed class MovieSearchResults
  {
    public readonly ImmutableList<Movie> movies;
    public readonly Pagination pagination;

    public MovieSearchResults(ImmutableList<Movie> movies, Pagination pagination){
      this.movies = movies;
      this.pagination = pagination;
    }
  }
}