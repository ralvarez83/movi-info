using System.Collections.Immutable;
using Shared.Domain.Criteria;

namespace Movies.Application.Dtos
{
  public readonly record struct MovieSearchResults (ImmutableList<Movie?> movies, Pagination pagination);
}