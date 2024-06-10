using Movies.Domain;
using Movies.Domain.ValueObjects;

namespace Movies.Application.MovieFind
{
    public class MoviFindById : MovieFinder
    {
      private MovieRepository _repository;
      private MovieId _movieId;
      public MoviFindById(MovieRepository repository, string movieId)
      {
        _repository = repository;
        _movieId = new MovieId(movieId);
      }

      public async Task<DTO.Movie?> find()
      {
          return TransformToMovieDTO(await _repository.findById(_movieId));
      }


      private DTO.Movie? TransformToMovieDTO (Movie? movieDomine){
        if (null == movieDomine)
          return null;

        return new DTO.Movie (
          movieDomine.id.value, 
          movieDomine.title, 
          movieDomine.overview, 
          movieDomine.adult, 
          (null != movieDomine.verticalImagePath)? movieDomine.verticalImagePath.AbsoluteUri:string.Empty,
          (null != movieDomine.horizontalImagePath)? movieDomine.horizontalImagePath.AbsoluteUri:string.Empty,
          (null != movieDomine.imdbLink)? movieDomine.imdbLink.AbsoluteUri:string.Empty,
          movieDomine.usersVote
        );
      }
    }
}