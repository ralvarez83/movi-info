using Domain.Movies;
using Domain.Movies.ValueObjects;

namespace Application.Movies
{
    public class MoviFindById : MovieFind
    {
      private MovieRepository _repository;
      private MovieId _movieId;
      public MoviFindById(MovieRepository repository, MovieId movieId)
      {
        _repository = repository;
        _movieId = movieId;
      }

      public async Task<Movie> find()
      {
          return await _repository.findById(_movieId);
      }
    }
}