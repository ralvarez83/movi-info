using Shared.Domain.Bus.Query;

namespace Movies.Application.MovieFind
{
  public class FindByIdMovieQuery (string movieId) : Query{
    public string MovieId {get;} = movieId;
  }

}