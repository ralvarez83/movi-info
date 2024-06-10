using System.Collections.Immutable;
using Shared.Domain.Criteria;
using MovieSearchResultsDomain = Movies.Domain.MovieSearchResults;

namespace Movies.Application.DTO
{
  public readonly record struct MovieSearchResults (ImmutableList<Movie?> movies, Pagination pagination);
}