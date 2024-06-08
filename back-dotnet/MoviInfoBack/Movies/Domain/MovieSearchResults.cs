using System.Collections.Immutable;
using Shared.Domain.Criteria;

namespace Movies.Domain
{
  public sealed class MovieSearchResults
  {
    public readonly ImmutableList<Movie> movies;
    public readonly Pagination pagination;

    public MovieSearchResults(ImmutableList<Movie> movies, Pagination? pagination){
      this.movies = movies;
      this.pagination = null != pagination ? pagination : new Pagination(0,0);
    }
  }
}