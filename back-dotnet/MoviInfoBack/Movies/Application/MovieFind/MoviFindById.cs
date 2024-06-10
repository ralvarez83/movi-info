using Movies.Domain;
using Movies.Domain.ValueObjects;

namespace Movies.Application.MovieFind
{
    public class MoviFindById : MovieFinder
    {
      private MovieRepository _repository;
      private MovieId _movieId;
      public MoviFindById(MovieRepository repository, MovieId movieId)
      {
        _repository = repository;
        _movieId = movieId;
      }

      public async Task<DTO.Movie?> find()
      {
          return TransformToMovieDTO(await _repository.findById(_movieId));
      }


      private DTO.Movie? TransformToMovieDTO (Movie? movieDomine){
        if (null == movieDomine)
          return null;

        return new DTO.Movie {
          id = movieDomine.id.value,
          title = movieDomine.title,
          overview = movieDomine.overview,
          adult = movieDomine.adult,
          verticalImagePath = (null != movieDomine.verticalImagePath)? movieDomine.verticalImagePath.AbsoluteUri:string.Empty,
          horizontalImagePath = (null != movieDomine.horizontalImagePath)? movieDomine.horizontalImagePath.AbsoluteUri:string.Empty,
          imdbLink = (null != movieDomine.imdbLink)? movieDomine.imdbLink.AbsoluteUri:string.Empty,
          usersVote = movieDomine.usersVote
        };
      }
    }
}