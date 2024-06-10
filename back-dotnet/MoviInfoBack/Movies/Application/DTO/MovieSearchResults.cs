using System.Collections.Immutable;
using Shared.Domain.Criteria;
using MovieSearchResultsDomain = Movies.Domain.MovieSearchResults;

namespace Movies.Application.DTO
{
  public record MovieSearchResults
  {
    public ImmutableList<Movie?> movies {get; set;}
    public Pagination pagination {get; set;}

  }
}