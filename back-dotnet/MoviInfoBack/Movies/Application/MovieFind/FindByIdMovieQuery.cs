using Shared.Domain.Bus.Query;

namespace Movies.Application.MovieFind
{
  public class FindByIdMovieQuery : Query{
    
    public string movieId {get; init;}

    public FindByIdMovieQuery (string movieId){
      this.movieId = movieId;
    }
  }

}