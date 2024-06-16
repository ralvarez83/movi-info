using Shared.Domain.Bus.Query;

namespace Movies.Application.MovieFind
{
  public record FindByIdMovieQuery (string movieId) : Query;

}